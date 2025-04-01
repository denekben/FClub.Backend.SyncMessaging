namespace Management.Domain.Entities.Pivots
{
    public sealed class ServiceTariff
    {
        public Guid Id { get; init; }
        public Guid ServiceId { get; init; }
        public Service Service { get; private set; }
        public Guid TariffId { get; init; }
        public Tariff Tariff { get; private set; }

        private ServiceTariff() { }

        private ServiceTariff(Guid serviceId, Guid tarrifId)
        {
            Id = Guid.NewGuid();
            ServiceId = serviceId;
            TariffId = tarrifId;
        }

        public static ServiceTariff Create(Guid serviceId, Guid tarrifId)
        {
            if (serviceId == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceTariff[serviceId]. Entered value {serviceId}");
            if (tarrifId == Guid.Empty)
                throw new DomainException($"Invalid value for ServiceTariff[tarrifId]. Entered value {tarrifId}");
            return new(serviceId, tarrifId);
        }
    }
}
