using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities.Pivots;

namespace Management.Domain.Entities
{
    public sealed class Service
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public List<ServiceTariff> ServiceTariffs { get; private set; } = [];
        public List<ServiceBranch> ServiceBranches { get; private set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; private set; }

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
    }
}
