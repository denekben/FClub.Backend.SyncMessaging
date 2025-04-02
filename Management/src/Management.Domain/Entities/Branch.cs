using FClub.Backend.Common.ValueObjects;
using Management.Domain.Entities.Pivots;

namespace Management.Domain.Entities
{
    public sealed class Branch
    {
        public Guid Id { get; init; }
        public string? Name { get; private set; }
        public Address Address { get; private set; }
        public List<ServiceBranch> ServiceBranches { get; private set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; private set; }

        private Branch() { }

        private Branch(string? name, Address address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            CreatedDate = DateTime.UtcNow;
        }

        public static Branch Create(string? name, string? country, string? city, string? street, string? houseNumber)
        {
            var address = Address.Create(country, city, street, houseNumber);

            return new(name, address);
        }
    }
}
