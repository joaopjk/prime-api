using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using prime_api.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Web.Api.Test.Usuario.QuandoRequisitarDelete
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "Não é possível realizar o Delete.")]
        public async Task Nao_e_possivel_invocar_a_controller_delete()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("", "");

            var result = await _controller.Delete(It.IsAny<Guid>());
            Assert.True(result is BadRequestObjectResult);

            var resultValue = ((BadRequestObjectResult)result).Value;
            Assert.NotNull(resultValue);
        }
    }
}
