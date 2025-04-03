using FClub.Backend.Common.Exceptions;

namespace Management.Domain.Entities
{
    public sealed class UserLog
    {
        public Guid Id { get; init; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        private UserLog() { }

        private UserLog(Guid appUserId, string text)
        {
            Id = Guid.NewGuid();
            AppUserId = appUserId;
            Text = text;
            CreatedDate = DateTime.UtcNow;
        }

        public static UserLog Create(Guid appUserId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new DomainException($"Invalid argument for UserLog[text]. Entered value: {text}");

            return new(appUserId, text);
        }
    }
}
