using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<SocialGroup> SocialGroups { get; set; }
        public DbSet<ServiceTariff> ServiceTariffs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<ServiceBranch> ServiceBranchs { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("FClub");

            var configuration = new ManagementConfiguration();

            modelBuilder.ApplyConfiguration<AppUser>(configuration);
            modelBuilder.ApplyConfiguration<Membership>(configuration);
            modelBuilder.ApplyConfiguration<SocialGroup>(configuration);
            modelBuilder.ApplyConfiguration<ServiceTariff>(configuration);
            modelBuilder.ApplyConfiguration<Role>(configuration);
            modelBuilder.ApplyConfiguration<Tariff>(configuration);
            modelBuilder.ApplyConfiguration<ServiceBranch>(configuration);
            modelBuilder.ApplyConfiguration<Branch>(configuration);
            modelBuilder.ApplyConfiguration<Client>(configuration);
            modelBuilder.ApplyConfiguration<Service>(configuration);

            base.OnModelCreating(modelBuilder);
        }
    }
}