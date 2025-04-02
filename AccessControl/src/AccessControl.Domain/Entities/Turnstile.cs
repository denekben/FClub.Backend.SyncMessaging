using AccessControll.Domain.Entities;
using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class Turnstile
    {
        public Guid Id { get; init; }
        public string? Name { get; set; }
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Turnstile() { }

        private Turnstile(string? name, Guid branchId, Guid serviceId)
        {
            Id = Guid.NewGuid();
            Name = name;
            BranchId = branchId;
            ServiceId = serviceId;
            CreatedDate = DateTime.UtcNow;
        }

        public static Turnstile Create(string? name, Guid branchId, Guid serviceId)
        {
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid value for Turnstile[branchId]. Entered value {branchId}");
            if (serviceId == Guid.Empty)
                throw new DomainException($"Invalid value for Turnstile[serviceId]. Entered value {serviceId}");

            return new(name, branchId, serviceId);
        }
    }
}
