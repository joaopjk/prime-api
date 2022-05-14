using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using prime_api.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Web.Api.Test.Usuario.QuandoRequisitarCreate
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "Não é possível realizar o Created.")]
        public async Task Nao_e_possivel_invocar_a_controller_create()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(x => x.Post(It.IsAny<UserDtoCreate>())).ReturnsAsync(
                new UserDtoCreateResult()
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                });
            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Name", "É um campo obrigatório.");
            var url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost:6666");
            _controller.Url = url.Object;

            var userDtoCreate = new UserDtoCreate()
            {
                Name = nome,
                Email = email
            };

            var result = await _controller.Post(userDtoCreate);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
