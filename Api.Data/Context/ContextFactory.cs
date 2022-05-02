using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<ContextApi>
    {
        public ContextApi CreateDbContext(string[] args)
        {
            //Usado para criar migrations em tempo de projeto
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            return new ContextApi(
                new DbContextOptionsBuilder<ContextApi>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options);
        }
    }
}
