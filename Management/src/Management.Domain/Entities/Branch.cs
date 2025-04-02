using FClub.Backend.Common.ValueObjects;
using Management.Domain.Entities.Pivots;

namespace Management.Domain.Entities
{
    public sealed class Branch
    {
        public Guid Id { get; init; }
        public string? Name { get; set; }
        public Address Address { get; set; }
        public List<ServiceBranch> ServiceBranches { get; set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

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

        public void UpdateDetails(string? name, string? country, string? city, string? street, string? houseNumber)
        {
            var address = Address.Create(country, city, street, houseNumber);

            Name = name;
            Address = address;
        }
    }
}
