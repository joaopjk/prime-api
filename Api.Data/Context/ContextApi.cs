using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Api.Data.Context
{
    [ExcludeFromCodeCoverage]
    public class ContextApi : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public ContextApi() { }
        public ContextApi(DbContextOptions<ContextApi> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserEntity>(new UserMap().Configure);
        }
    }
}
