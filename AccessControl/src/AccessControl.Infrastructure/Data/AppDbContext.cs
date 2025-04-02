using AccessControl.Domain.Entities;
using AccessControl.Domain.Entities.Pivots;
using AccessControll.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("FClub.AccessControl");

            var configuration = new AccessControlConfiguration();
            modelBuilder.ApplyConfiguration<ServiceBranch>(configuration);
            modelBuilder.ApplyConfiguration<ServiceTariff>(configuration);
            modelBuilder.ApplyConfiguration<Branch>(configuration);
            modelBuilder.ApplyConfiguration<Client>(configuration);
            modelBuilder.ApplyConfiguration<EntryLog>(configuration);
            modelBuilder.ApplyConfiguration<Membership>(configuration);
            modelBuilder.ApplyConfiguration<Service>(configuration);
            modelBuilder.ApplyConfiguration<Tariff>(configuration);
            modelBuilder.ApplyConfiguration<Turnstile>(configuration);

            base.OnModelCreating(modelBuilder);
        }
    }
}
