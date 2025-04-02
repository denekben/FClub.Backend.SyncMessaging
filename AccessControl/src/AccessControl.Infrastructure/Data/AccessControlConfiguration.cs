using AccessControl.Domain.Entities;
using AccessControl.Domain.Entities.Pivots;
using AccessControll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.Infrastructure.Data
{
    internal sealed class AccessControlConfiguration :
        IEntityTypeConfiguration<ServiceBranch>, IEntityTypeConfiguration<ServiceTariff>,
        IEntityTypeConfiguration<Branch>, IEntityTypeConfiguration<Client>,
        IEntityTypeConfiguration<EntryLog>, IEntityTypeConfiguration<Membership>,
        IEntityTypeConfiguration<Service>, IEntityTypeConfiguration<Tariff>,
        IEntityTypeConfiguration<Turnstile>
    {
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

        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            builder.HasKey(t => t.Id);

            builder.ToTable("Tariffs");
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

        public void Configure(EntityTypeBuilder<EntryLog> builder)
        {
            builder.HasKey(el => el.Id);

            builder
                .HasOne(el => el.Client)
                .WithMany(c => c.EntryLogs)
                .HasForeignKey(el => el.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("UserLogs");
        }

        public void Configure(EntityTypeBuilder<Turnstile> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .HasOne(t => t.Branch)
                .WithMany(b => b.Turnstiles)
                .HasForeignKey(t => t.BranchId);

            builder
                .HasOne(t => t.Service)
                .WithMany(b => b.Turnstiles)
                .HasForeignKey(t => t.ServiceId);

            builder.ToTable("Turnstiles");
        }
    }
}
