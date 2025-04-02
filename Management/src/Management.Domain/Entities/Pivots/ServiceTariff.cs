using FClub.Backend.Common.Exceptions;

namespace Management.Domain.Entities.Pivots
{
    public sealed class ServiceTariff
    {
        public Guid Id { get; init; }
        public Guid ServiceId { get; init; }
        public Service Service { get; set; }
        public Guid TariffId { get; init; }
        public Tariff Tariff { get; set; }

        private ServiceTariff() { }

        private ServiceTariff(Guid serviceId, Guid tarrifId)
        {
            Id = Guid.NewGuid();
            ServiceId = serviceId;
            TariffId = tarrifId;
        }

        public static ServiceTariff Create(Guid serviceId, Guid tariffId)
        {
            if (serviceId == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceTariff[serviceId]. Entered value {serviceId}");
            if (tariffId == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceTariff[tariffId]. Entered value {tariffId}");
            return new(serviceId, tariffId);
        }
    }
}
