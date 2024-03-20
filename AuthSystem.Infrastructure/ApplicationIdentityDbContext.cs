using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Domain.CountryAggregate;
using AuthSystem.Infrastructure.Identity;
using AuthSystem.Infrastructure.EntityConfigurations;

namespace AuthSystem.Infrastructure
{
    public class ApplicationIdentityDbContext : IdentityDbContext<User>
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }

        public ApplicationIdentityDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserEntityConfigurations());
        }
    }
}