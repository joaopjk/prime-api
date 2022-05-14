using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTest>
    {
        private readonly ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "Crud de usuário.")]
        public async Task E_possivel_CRUD_Usuario()
        {
            using (var context = _serviceProvider.GetService<ContextApi>())
            {
                var _repositorio = new UserImplementation(context);

                //Create
                var _entity = new UserEntity()
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };
                var registroCriado = await _repositorio.InsertAsync(_entity);
                Assert.NotNull(registroCriado);
                Assert.Equal(_entity.Email, registroCriado.Email);
                Assert.Equal(_entity.Name, registroCriado.Name);
                Assert.False(registroCriado.Id == Guid.Empty);

                //Update
                _entity.Name = Faker.Name.First();
                var registoAtulizado = await _repositorio.UpdateAsync(_entity);
                Assert.NotNull(registoAtulizado);
                Assert.Equal(_entity.Email, registoAtulizado.Email);
                Assert.Equal(_entity.Name, registoAtulizado.Name);

                //Read
                var resitroExiste = await _repositorio.ExistAsync(registoAtulizado.Id);
                Assert.True(resitroExiste);

                var resitroSelecionado = await _repositorio.SelectAsync(registoAtulizado.Id);
                Assert.NotNull(resitroSelecionado);
                Assert.Equal(resitroSelecionado.Email, registoAtulizado.Email);
                Assert.Equal(resitroSelecionado.Name, registoAtulizado.Name);

                var todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() > 0);

                var registroPorEmail = await _repositorio.FindByLogin(registoAtulizado.Email);
                Assert.NotNull(registroPorEmail);

                //Delete
                var removeu = await _repositorio.DeleteAsync(resitroSelecionado.Id);
                Assert.True(removeu);

            }
        }
    }
}
