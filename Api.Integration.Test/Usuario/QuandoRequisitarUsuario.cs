using Api.Domain.Dtos.User;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test.Usuario
{
    public class QuandoRequisitarUsuario : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }
        [Fact(DisplayName = "É possivel realizar CRUD de usuário")]
        public async Task E_possivel_realizar_CRUD_usuario()
        {
            await AdicionarToken();
            _name = Faker.Name.FullName();
            _email = Faker.Internet.Email();

            var userDto = new UserDtoCreate()
            {
                Name = _name,
                Email = _email
            };

            //Post
            var responseCreate = await PostJsonAsync(userDto, $"{hostApi}users", client);
            var registroPost = JsonConvert.DeserializeObject<UserDtoCreateResult>(await responseCreate.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.Created, responseCreate.StatusCode);
            Assert.Equal(_name, registroPost.Name);
            Assert.Equal(_email, registroPost.Email);
            Assert.True(registroPost.Id != default);

            //getAll
            response = await client.GetAsync($"{hostApi}users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var listaUsuario = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(listaUsuario);
            Assert.True(listaUsuario.Count() > 0);
            Assert.True(listaUsuario.Where(x => x.Id == registroPost.Id).Any());

            //Put
            var updateUserDto = new UserDtoUpdate()
            {
                Id = registroPost.Id,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}users", stringContent);
            var registroAtualizado = JsonConvert.DeserializeObject<UserDtoUpdateResult>(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(registroAtualizado.Name, _name);
            Assert.NotEqual(registroAtualizado.Email, _email);

            //Get
            response = await client.GetAsync($"{hostApi}users/GetWithId/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var getusuario = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(getusuario);

            //Delete
            response = await client.DeleteAsync($"{hostApi}users/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
