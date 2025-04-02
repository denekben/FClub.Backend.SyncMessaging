using FClub.Backend.Common.Exceptions;

namespace Notifications.Domain.Entities
{
    public sealed class Notification
    {
        public Guid Id { get; init; }
        public string Title { get; set; }
        public string Text { get; set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Notification() { }

        private Notification(string title, string text)
        {
            Id = Guid.NewGuid();
            Title = title;
            Text = text;
            CreatedDate = DateTime.UtcNow;
        }

        public static Notification Create(string title, string text)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException($"Invalid value for Notification[title]. Entered value {title}");
            if (string.IsNullOrEmpty(text))
                throw new DomainException($"Invalid value for Notification[text]. Entered value {text}");

            return new(title, text);
        }
    }
}
