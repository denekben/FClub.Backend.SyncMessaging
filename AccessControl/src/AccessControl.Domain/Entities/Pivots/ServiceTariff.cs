using AccessControll.Domain.Entities;
using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities.Pivots
{
    public sealed class ServiceTariff
    {
        public Guid Id { get; init; }
        public Guid ServiceId { get; init; }
        public Service Service { get; set; }
        public Guid TariffId { get; init; }
        public Tariff Tariff { get; set; }

        private ServiceTariff() { }

        private ServiceTariff(Guid id, Guid serviceId, Guid tarrifId)
        {
            Id = id;
            ServiceId = serviceId;
            TariffId = tarrifId;
        }

        public static ServiceTariff Create(Guid id, Guid serviceId, Guid tariffId)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceTariff[id]. Entered value {id}");
            if (serviceId == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceTariff[serviceId]. Entered value {serviceId}");
            if (tariffId == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceTariff[tariffId]. Entered value {tariffId}");
            return new(id, serviceId, tariffId);
        }
    }
}
