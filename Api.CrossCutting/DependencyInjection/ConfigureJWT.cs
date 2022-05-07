using Api.Domain.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureJWT
    {
        public static void ConfigureJWTInjection(this IServiceCollection service)
        {
            var signingConfiguration = new SigningConfiguration();
            service.AddSingleton(signingConfiguration);
        }
    }
}
