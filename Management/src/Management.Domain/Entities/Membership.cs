using FClub.Backend.Common.Exceptions;

namespace Management.Domain.Entities
{
    public sealed class Membership
    {
        public Guid Id { get; init; }
        public Guid TariffId { get; set; }
        public Tariff Tariff { get; set; }
        public DateTime ExpiresDate { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Membership(Guid tariffId, DateTime expiresDate, Guid clientId)
        {
            Id = Guid.NewGuid();
            TariffId = tariffId;
            ExpiresDate = expiresDate;
            ClientId = clientId;
            CreatedDate = DateTime.UtcNow;
        }

        public static Membership Create(Guid tariffId, DateTime expiresDate, Guid clientId)
        {
            if (tariffId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[tariffId]. Entered value {tariffId}");
            if (clientId == Guid.Empty)
                throw new DomainException($"Invalid value for Membership[clientId]. Entered value {clientId}");
            if (expiresDate <= DateTime.UtcNow)
                throw new DomainException($"Invalid value for Membership[expiresDate]. Entered value {expiresDate}");

            return new(tariffId, expiresDate, clientId);
        }
    }
}
