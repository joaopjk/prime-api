using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoGet : UsuariosTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GET")]
        public async Task E_possivel_executar_metodo_get()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Get(IdUsuario)).ReturnsAsync(userDto);
            _service = _serviceMock.Object;
            var result = await _service.Get(IdUsuario);
            Assert.NotNull(result);
            Assert.True(result.Id == IdUsuario);
            Assert.Equal(NomeUsuario, result.Name);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto)null));
            _service = _serviceMock.Object;
            var _record = await _service.Get(IdUsuario);
            Assert.Null(_record);
        }
    }
}
