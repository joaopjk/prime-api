using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using prime_api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Web.Api.Test.Usuario.QuandoRequisitarGet
{
    public class Retorno_Get
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possível realizar o Get.")]
        public async Task E_possivel_invocar_a_controller_get()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(new UserDto
            {
                Id = Guid.NewGuid(),
                Name = nome,
                Email = email,
                CreateAt = DateTime.UtcNow
            });

            _controller = new UsersController(serviceMock.Object);

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as UserDto;
            Assert.NotNull(resultValue);
            Assert.Equal(resultValue.Name, nome);
            Assert.Equal(resultValue.Email, email);
        }

        [Fact(DisplayName = "É possível realizar o GetAll.")]
        public async Task E_possivel_invocar_a_controller_getAll()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(populateList());

            _controller = new UsersController(serviceMock.Object);

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as List<UserDto>;
            Assert.True(resultValue.Any());
            Assert.True(resultValue.Count == 5);
        }

        private List<UserDto> populateList()
        {
            List<UserDto> lista = new();
            for (int i = 0; i < 5; i++)
            {
                lista.Add(new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow
                });
            }
            return lista;
        }
    }
}
