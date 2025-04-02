using FClub.Backend.Common.Exceptions;

namespace AccessControl.Domain.Entities
{
    public sealed class EntryLog
    {
        public Guid Id { get; init; }
        public Guid ClientId { get; private set; }
        public Client Client { get; private set; }
        public string Text { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }

        private EntryLog() { }

        private EntryLog(Guid clientId, string text)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            Text = text;
            CreatedDate = DateTime.UtcNow;
        }

        public static EntryLog Create(Guid clientId, string text)
        {
            if (clientId == Guid.Empty)
                throw new DomainException($"Invalid argument for EntryLog[clientId]. Entered value: {clientId}");
            if (string.IsNullOrWhiteSpace(text))
                throw new DomainException($"Invalid argument for EntryLog[text]. Entered value: {text}");

            return new(clientId, text);
        }
    }
}
