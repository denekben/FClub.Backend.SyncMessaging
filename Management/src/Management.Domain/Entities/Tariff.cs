using Management.Domain.Entities.Pivots;

namespace Management.Domain.Entities
{
    public sealed class Tariff
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public Dictionary<int, int> PriceForNMonths { get; private set; } = [];
        public Dictionary<Guid, int>? DiscountForSocialGroup { get; private set; } = [];
        public List<ServiceTariff> ServiceTariffs { get; private set; } = [];
        public List<Membership> Memberships { get; private set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; private set; }

        private Tariff() { }

        private Tariff(string name, Dictionary<int, int> priceForNMonths, Dictionary<Guid, int>? discountForSocialGroup)
        {
            Id = Guid.NewGuid();
            Name = name;
            PriceForNMonths = priceForNMonths;
            DiscountForSocialGroup = discountForSocialGroup;
            CreatedDate = DateTime.UtcNow;
        }

        public static Tariff Create(string name, Dictionary<int, int> priceForNMonths, Dictionary<Guid, int>? discountForSocialGroup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Tariff[name]. Entered value: {name}");
            if (priceForNMonths == null || !priceForNMonths.Any() || priceForNMonths.Any(x => x.Key < 0 || x.Value < 0))
                throw new DomainException($"Invalid argument for Tariff[priceForNMonths]. Entered value: {priceForNMonths}");
            if (discountForSocialGroup?.Any(x => x.Key == default || x.Value < 0) == true)
                throw new DomainException($"Invalid argument for Tariff[discountForSocialGroup]. Entered value: {discountForSocialGroup}");

            return new(name, priceForNMonths, discountForSocialGroup);
        }
    }
}
