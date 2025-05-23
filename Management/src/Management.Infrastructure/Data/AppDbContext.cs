﻿using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Microsoft.EntityFrameworkCore;
using DomainService = Management.Domain.Entities.Service;

namespace Management.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly Seed _seeder;

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<SocialGroup> SocialGroups { get; set; }
        public DbSet<ServiceTariff> ServiceTariffs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<ServiceBranch> ServiceBranches { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DomainService> Services { get; set; }
        public DbSet<StatisticNote> StatisticNotes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, Seed seeder) : base(options)
        {
            _seeder = seeder;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            modelBuilder.ApplyConfiguration<DomainService>(configuration);
            modelBuilder.ApplyConfiguration<StatisticNote>(configuration);

            _seeder.SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}