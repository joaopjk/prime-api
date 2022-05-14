using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using prime_api.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Web.Api.Test.Usuario.QuantoRequisitarUpdate
{
    public class Retorno_Update
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possível realizar o Updated.")]
        public async Task E_possivel_invocar_a_controller_update()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(x => x.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(
                new UserDtoUpdateResult()
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    UpdateAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object);

            var userDtoUpdate = new UserDtoUpdate
            {
                Id = Guid.NewGuid(),
                Name = nome,
                Email = email
            };

            var result = await _controller.Put(userDtoUpdate);
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as UserDtoUpdateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(userDtoUpdate.Name, resultValue.Name);
            Assert.Equal(userDtoUpdate.Email, resultValue.Email);
        }
    }
}
