using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.ValueObjects;
using System.Text.RegularExpressions;

namespace Notifications.Domain.Entities
{
    public sealed class Client
    {
        private readonly static Regex _phonePattern = new(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", RegexOptions.IgnoreCase);
        private readonly static Regex _emailPattern = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);

        public Guid Id { get; init; }
        public FullName FullName { get; private set; }
        public string? Phone { get; private set; }
        public string Email { get; private set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; private set; }

        private Client() { }

        private Client(Guid id, FullName fullName, string? phone, string email)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
            Email = email;
            CreatedDate = DateTime.UtcNow;
        }

        public static Client Create(Guid id, string firstName, string secondName, string? patronymic,
            string? phone, string email)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for Client[id]. Entered value {id}");
            if (phone != null && !_phonePattern.IsMatch(phone))
                throw new DomainException($"Invalid value for Client[phone]. Entered value {phone}");
            if (string.IsNullOrWhiteSpace(email) || !_emailPattern.IsMatch(email))
                throw new DomainException($"Invalid value for Client[email]. Entered value {email}");
            var fullName = FullName.Create(firstName, secondName, patronymic);

            return new(id, fullName, phone, email);
        }
    }
}
