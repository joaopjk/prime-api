using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoGetAll : UsuariosTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GET")]
        public async Task E_possivel_executar_metodo_getAll()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listaUsuarios);
            _service = _serviceMock.Object;
            var result = await _service.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Any());

            var listResults = new List<UserDto>();
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listResults.AsEnumerable);
            _service = _serviceMock.Object;
            var resultEmpty = await _service.GetAll();
            Assert.Empty(resultEmpty);
            Assert.False(resultEmpty.Any());
        }
    }
}
