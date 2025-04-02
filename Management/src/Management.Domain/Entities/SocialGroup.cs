using FClub.Backend.Common.Exceptions;

namespace Management.Domain.Entities
{
    public sealed class SocialGroup
    {
        public Guid Id { get; init; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private SocialGroup() { }

        private SocialGroup(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedDate = DateTime.UtcNow;
        }

        public static SocialGroup Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"Invalid argument for Service[name]. Entered value: {name}");

            return new(name);
        }
    }
}
