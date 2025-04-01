using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Management.Infrastructure.Data
{
    public class ManagementConfiguration : IEntityTypeConfiguration<AppUser>,
        IEntityTypeConfiguration<Membership>, IEntityTypeConfiguration<SocialGroup>,
        IEntityTypeConfiguration<ServiceTariff>, IEntityTypeConfiguration<Role>,
        IEntityTypeConfiguration<Tariff>, IEntityTypeConfiguration<ServiceBranch>,
        IEntityTypeConfiguration<Branch>, IEntityTypeConfiguration<Client>,
        IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(au => au.Id);

            builder
                .OwnsOne(au => au.FullName, ownedBuilder =>
                {
                    ownedBuilder.Property(fn => fn.FirstName);
                    ownedBuilder.Property(fn => fn.SecondName);
                    ownedBuilder.Property(fn => fn.Patronymic);
                });

            builder
                .HasOne(au => au.Role)
                .WithMany(r => r.AppUsers)
                .HasForeignKey(au => au.RoleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("AppUsers");
        }

        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .HasOne(m => m.Tariff)
                .WithMany(t => t.Memberships)
                .HasForeignKey(m => m.TariffId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(m => m.Client)
                .WithOne(c => c.Membership)
                .HasForeignKey<Membership>(m => m.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Memberships");
        }

        public void Configure(EntityTypeBuilder<ServiceTariff> builder)
        {
            builder.HasKey(st => st.Id);

            builder
                .HasOne(st => st.Service)
                .WithMany(s => s.ServiceTariffs)
                .HasForeignKey(st => st.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(st => st.Tariff)
                .WithMany(t => t.ServiceTariffs)
                .HasForeignKey(st => st.TariffId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ServiceTariffs");
        }

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasData(Seed.Roles);

            builder.ToTable("Roles");
        }

        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .Property(t => t.PriceForNMonths)
                .HasConversion(
                    pfnm => JsonSerializer.Serialize(pfnm, (JsonSerializerOptions)null!),
                    pfnm => JsonSerializer.Deserialize<Dictionary<int, int>>(pfnm, (JsonSerializerOptions)null!)!
                );

            builder
                .Property(t => t.DiscountForSocialGroup)
                .HasConversion(
                    dfsg => JsonSerializer.Serialize(dfsg, (JsonSerializerOptions)null!),
                    dfsg => JsonSerializer.Deserialize<Dictionary<Guid, int>?>(dfsg, (JsonSerializerOptions)null!)
                );

            builder.ToTable("Tariffs");
        }

        public void Configure(EntityTypeBuilder<SocialGroup> builder)
        {
            builder.HasKey(sg => sg.Id);

            builder.ToTable("SocialGroups");
        }

        public void Configure(EntityTypeBuilder<ServiceBranch> builder)
        {
            builder.HasKey(sb => sb.Id);

            builder
                .HasOne(sb => sb.Service)
                .WithMany(s => s.ServiceBranches)
                .HasForeignKey(sb => sb.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(sb => sb.Branch)
                .WithMany(b => b.ServiceBranches)
                .HasForeignKey(sb => sb.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ServiceBranches");
        }

        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .OwnsOne(b => b.Address, ownedBuilder =>
                {
                    ownedBuilder.Property(a => a.Country);
                    ownedBuilder.Property(a => a.City);
                    ownedBuilder.Property(a => a.Street);
                    ownedBuilder.Property(a => a.HouseNumber);
                });

            builder.ToTable("Branches");
        }

        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .OwnsOne(c => c.FullName, ownedBuilder =>
                {
                    ownedBuilder.Property(fn => fn.FirstName);
                    ownedBuilder.Property(fn => fn.SecondName);
                    ownedBuilder.Property(fn => fn.Patronymic);
                });

            builder
                .HasOne(c => c.Membership)
                .WithOne(m => m.Client)
                .HasForeignKey<Client>(c => c.MembershipId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Clients");
        }

        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Id);

            builder.ToTable("Services");
        }
    }
}
