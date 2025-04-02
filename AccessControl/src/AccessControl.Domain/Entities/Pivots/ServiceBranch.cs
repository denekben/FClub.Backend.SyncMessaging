using AccessControll.Domain.Entities;
using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities.Pivots
{
    public sealed class ServiceBranch
    {
        public Guid Id { get; init; }
        public Guid ServiceId { get; init; }
        public Service Service { get; private set; }
        public Guid BranchId { get; init; }
        public Branch Branch { get; private set; }

        private ServiceBranch() { }

        private ServiceBranch(Guid id, Guid serviceId, Guid branchId)
        {
            Id = id;
            ServiceId = serviceId;
            BranchId = branchId;
        }

        public static ServiceBranch Create(Guid id, Guid serviceId, Guid branchId)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceBranch[id]. Entered value {id}");
            if (serviceId == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceBranch[serviceId]. Entered value {serviceId}");
            if (branchId == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceBranch[branchId]. Entered value {branchId}");
            return new(id, serviceId, branchId);
        }
    }
}
