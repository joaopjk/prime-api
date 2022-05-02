using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
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
