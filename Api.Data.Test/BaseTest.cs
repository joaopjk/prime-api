using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Api.Data.Test
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseTest
    {
        public BaseTest() { }
    }

    public class DbTest : IDisposable
    {
        private string _dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; set; }

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ContextApi>(x =>
            {
                x.UseSqlServer($"Server=127.0.0.1,1433;Database={_dataBaseName};user id=SA;Password=Root@123root");
            }, ServiceLifetime.Transient);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            using var context = ServiceProvider.GetService<ContextApi>();
            context.Database.EnsureCreated();

        }
        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<ContextApi>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
