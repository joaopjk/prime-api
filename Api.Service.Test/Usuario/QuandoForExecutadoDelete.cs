using Api.Domain.Interfaces.Services.User;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoDelete : UsuariosTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Delete")]
        public async Task E_possivel_executar_metodo_create()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;
            var result = await _service.Delete(It.IsAny<Guid>());
            Assert.True(result);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;
            result = await _service.Delete(It.IsAny<Guid>());
            Assert.False(result);
        }
    }
}
