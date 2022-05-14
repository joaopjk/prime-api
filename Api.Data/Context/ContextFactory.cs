using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace Api.Data.Context
{
    [ExcludeFromCodeCoverage]
    public class ContextFactory : IDesignTimeDbContextFactory<ContextApi>
    {
        public ContextApi CreateDbContext(string[] args)
        {
            //Usado para criar migrations em tempo de projeto
            var connectionString = "Server=127.0.0.1,1433;Database=PrimeAPI;user id=SA;Password=Root@123root";
            return new ContextApi(
                new DbContextOptionsBuilder<ContextApi>()
                .UseSqlServer(connectionString).Options);
        }
    }
}
