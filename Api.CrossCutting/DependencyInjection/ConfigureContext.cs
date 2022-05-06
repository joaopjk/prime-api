using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureContext
    {
        public static void ConfigureContextInjection(this IServiceCollection service)
        {
            service.AddDbContext<ContextApi>(
                options => options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_SERVER"))
                );
        }
    }
}
