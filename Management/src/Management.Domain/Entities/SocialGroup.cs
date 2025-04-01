namespace Management.Domain.Entities
{
    public sealed class SocialGroup
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; private set; }

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
