using AccessControl.Domain.Entities.Pivots;
using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class Tariff
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public bool AllowMultiBranches { get; set; }
        public List<ServiceTariff> ServiceTariffs { get; set; } = [];
        public List<Membership> Memberships { get; set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Tariff() { }

        private Tariff(Guid id, string name, bool allowMultiBranches)
        {
            Id = id;
            Name = name;
            CreatedDate = DateTime.UtcNow;
            AllowMultiBranches = allowMultiBranches;
        }

        public static Tariff Create(Guid id, string name, bool allowMultiBranches)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for Tariff[id]. Entered value {id}");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Tariff[name]. Entered value: {name}");

            return new(id, name, allowMultiBranches);
        }
    }
}
