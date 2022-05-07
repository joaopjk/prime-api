using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _repository;
        private readonly SigningConfiguration _signingConfiguration;

        public LoginService(IUserRepository repository,
                            SigningConfiguration signingConfiguration)
        {
            _repository = repository;
            _signingConfiguration = signingConfiguration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);

                if (baseUser == null)
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar !"
                    };
                else
                {
                    var identity = new ClaimsIdentity(
                            new GenericIdentity(user.Email),
                            new[]
                            {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),// Jti é o ID do Token
                                new Claim(JwtRegisteredClaimNames.UniqueName,user.Email)
                            }
                        );

                    DateTime createDate = DateTime.UtcNow;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(
                        Convert.ToDouble(Environment.GetEnvironmentVariable("Seconds")));

                    var handler = new JwtSecurityTokenHandler();
                    var token = CreateToken(identity, createDate, expirationDate, handler);

                    return SuccesObject(createDate, expirationDate, token, user);
                }
            }
            else
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar !"
                };
        }

        private string CreateToken(ClaimsIdentity identity,
                            DateTime createDate,
                            DateTime expirationDate,
                            JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            return handler.WriteToken(securityToken);
        }

        private object SuccesObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new
            {
                authenticated = true,
                createDate = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = user.Email,
                message = "Usuário logado com sucesso."
            };
        }
    }
}
