using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Login
{
    public class QuandoForExecutadoFindByLogin
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;

        [Fact(DisplayName = "É possível executar o Método FindByLogin")]
        public async Task E_possivel_executar_metodo_FindByLogin()
        {
            var email = Faker.Internet.Email();
            var objetoRetorno = new
            {
                authenticated = true,
                create = DateTime.UtcNow,
                expiration = DateTime.UtcNow.AddMinutes(30),
                accessToken = Guid.NewGuid(),
                userName = email,
                name = Faker.Name.FullName(),
                message = ""
            };
            var loginDto = new LoginDto
            {
                Email = email,
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(x => x.FindByLogin(loginDto)).ReturnsAsync(objetoRetorno);
            _service = _serviceMock.Object;

            var result = await _service.FindByLogin(loginDto);

            Assert.NotNull(result);
        }
    }
}
