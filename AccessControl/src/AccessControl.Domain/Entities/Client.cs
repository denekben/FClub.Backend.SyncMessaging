using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.ValueObjects;
using System.Text.RegularExpressions;

namespace AccessControl.Domain.Entities
{
    public sealed class Client
    {
        private readonly static Regex _phonePattern = new(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", RegexOptions.IgnoreCase);
        private readonly static Regex _emailPattern = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);

        public Guid Id { get; init; }
        public FullName FullName { get; private set; }
        public string? Phone { get; private set; }
        public string Email { get; private set; }
        public bool AllowEntry { get; private set; }
        public bool IsStaff { get; private set; }
        public Guid? MembershipId { get; private set; }
        public Membership? Membership { get; private set; }
        public List<EntryLog> EntryLogs { get; private set; } = [];

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; private set; }

        private Client() { }

        private Client(Guid id, FullName fullName, string? phone, string email, bool allowEntry, bool isStaff, Guid? membershipId)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
            Email = email;
            AllowEntry = allowEntry;
            MembershipId = membershipId;
            CreatedDate = DateTime.UtcNow;
        }

        public static Client Create(Guid id, string firstName, string secondName, string? patronymic,
            string? phone, string email, bool allowEntry, bool isStaff, Guid? membershipId)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for Client[id]. Entered value {id}");
            if (phone != null && !_phonePattern.IsMatch(phone))
                throw new DomainException($"Invalid value for Client[phone]. Entered value {phone}");
            if (string.IsNullOrWhiteSpace(email) || !_emailPattern.IsMatch(email))
                throw new DomainException($"Invalid value for Client[email]. Entered value {email}");
            if (membershipId == Guid.Empty)
                throw new DomainException($"Invalid value for Client[membershipId]. Entered value {membershipId}");
            var fullName = FullName.Create(firstName, secondName, patronymic);

            return new(id, fullName, phone, email, allowEntry, isStaff, membershipId);
        }
    }
}
