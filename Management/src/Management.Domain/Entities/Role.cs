namespace Management.Domain.Entities
{
    public sealed class Role
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public List<AppUser> AppUsers { get; private set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; private set; }

        public static Role Admin => new(nameof(Admin));
        public static Role Manager => new(nameof(Manager));

        private Role() { }

        private Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedDate = DateTime.UtcNow;
        }

        public Role Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException($"Invalid argument for Role[name]. Entered value: {name}");
            }

            return new(name);
        }
    }
}
