using Api.Domain.Dtos;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto email);
    }
}
