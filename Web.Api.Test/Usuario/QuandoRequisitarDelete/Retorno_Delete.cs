using Api.Domain.Interfaces.Services.User;
using Moq;
using prime_api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Web.Api.Test.Usuario.QuandoRequisitarDelete
{
    public class Retorno_Delete
    {
        private UsersController _controller;

        [Fact(DisplayName = "Não é possível realizar o Delete.")]
        public async Task Nao_e_possivel_invocar_a_controller_delete()
        {
            var serviceMock = new Mock<IUserService>();
        }
    }
}
