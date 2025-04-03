using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities.Pivots;

namespace Management.Domain.Entities
{
    public sealed class Service
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public List<ServiceTariff> ServiceTariffs { get; set; } = [];
        public List<ServiceBranch> ServiceBranches { get; set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Service() { }

        private Service(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedDate = DateTime.UtcNow;
        }

        public static Service Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException($"Invalid argument for Service[name]. Entered value: {name}");
            }

            return new(name);
        }

        public void UpdateDetails(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException($"Invalid argument for Service[name]. Entered value: {name}");
            }
            Name = name;
        }
    }
}
