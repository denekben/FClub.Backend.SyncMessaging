using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities.Pivots;

namespace Management.Domain.Entities
{
    public sealed class Tariff
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public Dictionary<int, int> PriceForNMonths { get; set; } = [];
        public Dictionary<Guid, int>? DiscountForSocialGroup { get; set; } = [];
        public bool AllowMultiBranches { get; set; }
        public List<ServiceTariff> ServiceTariffs { get; set; } = [];
        public List<Membership> Memberships { get; set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Tariff() { }

        private Tariff(string name, Dictionary<int, int> priceForNMonths, Dictionary<Guid, int>? discountForSocialGroup, bool allowMultiBranches)
        {
            Id = Guid.NewGuid();
            Name = name;
            PriceForNMonths = priceForNMonths;
            DiscountForSocialGroup = discountForSocialGroup;
            CreatedDate = DateTime.UtcNow;
            AllowMultiBranches = allowMultiBranches;
        }

        public static Tariff Create(string name, Dictionary<int, int> priceForNMonths, Dictionary<Guid, int>? discountForSocialGroup, bool allowMultiBranches)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Tariff[name]. Entered value: {name}");
            if (priceForNMonths == null || !priceForNMonths.Any() || priceForNMonths.Any(x => x.Key < 0 || x.Value < 0))
                throw new DomainException($"Invalid argument for Tariff[priceForNMonths]. Entered value: {priceForNMonths}");
            if (discountForSocialGroup?.Any(x => x.Key == default || x.Value < 0) == true)
                throw new DomainException($"Invalid argument for Tariff[discountForSocialGroup]. Entered value: {discountForSocialGroup}");

            return new(name, priceForNMonths, discountForSocialGroup, allowMultiBranches);
        }

        public void UpdateDetails(string name, Dictionary<int, int> priceForNMonths, Dictionary<Guid, int>? discountForSocialGroup, bool allowMultiBranches)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Tariff[name]. Entered value: {name}");
            if (priceForNMonths == null || !priceForNMonths.Any() || priceForNMonths.Any(x => x.Key < 0 || x.Value < 0))
                throw new DomainException($"Invalid argument for Tariff[priceForNMonths]. Entered value: {priceForNMonths}");
            if (discountForSocialGroup?.Any(x => x.Key == default || x.Value < 0) == true)
                throw new DomainException($"Invalid argument for Tariff[discountForSocialGroup]. Entered value: {discountForSocialGroup}");

            Name = name;
            PriceForNMonths = priceForNMonths;
            DiscountForSocialGroup = discountForSocialGroup;
            AllowMultiBranches = allowMultiBranches;
        }
    }
}
