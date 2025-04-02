using FClub.Backend.Common.Exceptions;

namespace Management.Domain.Entities
{
    public sealed class UserLog
    {
        public Guid Id { get; init; }
        public Guid AppUserId { get; private set; }
        public AppUser AppUser { get; private set; }
        public string Text { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }

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
            if (appUserId == Guid.Empty)
                throw new DomainException($"Invalid argument for UserLog[appUserId]. Entered value: {appUserId}");
            if (string.IsNullOrWhiteSpace(text))
                throw new DomainException($"Invalid argument for UserLog[text]. Entered value: {text}");

            return new(appUserId, text);
        }
    }
}
