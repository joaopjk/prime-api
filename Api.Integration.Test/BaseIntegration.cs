using Api.CrossCutting.DependencyInjection;
using Api.Data.Context;
using Api.Domain.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using prime_api;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        public ContextApi context { get; private set; }
        public HttpClient client { get; private set; }
        public IMapper mapper { get; set; }
        public string hostApi { get; set; }
        public HttpResponseMessage response { get; set; }
        public BaseIntegration()
        {
            hostApi = "http://localhost:5050/api/";
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();
            var server = new TestServer(builder);
            context = server.Host.Services.GetService(typeof(ContextApi)) as ContextApi;
            context.Database.Migrate();
            mapper = ConfigureMapping.MapperConfigure().CreateMapper();
            client = server.CreateClient();
        }

        public void Dispose()
        {
            context?.Dispose();
            client?.Dispose();
        }

        public async Task AdicionarToken()
        {
            var loginDto = new LoginDto()
            {
                Email = "joaopjk48@gmail.com"
            };

            var resultLogin = await PostJsonAsync(loginDto, $"{hostApi}login", client);
            var loginObject = JsonConvert.DeserializeObject<LoginResponseDto>(await resultLogin.Content.ReadAsStringAsync());

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginObject.accessToken);
        }

        public static async Task<HttpResponseMessage> PostJsonAsync(object dataclass, string url, HttpClient client)
        {
            return await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(dataclass), Encoding.UTF8, "application/json"));
        }
    }
}
