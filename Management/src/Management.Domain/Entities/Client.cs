﻿using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.ValueObjects;
using System.Text.RegularExpressions;

namespace Management.Domain.Entities
{
    public sealed class Client
    {
        private readonly static Regex _phonePattern = new(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", RegexOptions.IgnoreCase);
        private readonly static Regex _emailPattern = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);

        public Guid Id { get; init; }
        public FullName FullName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public bool AllowEntry { get; set; }
        public bool AllowNotifications { get; set; }
        public Guid? MembershipId { get; set; }
        public Membership? Membership { get; set; }
        public Guid? SocialGroupId { get; set; }
        public SocialGroup SocialGroup { get; set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private Client() { }

        private Client(FullName fullName, string? phone, string email, bool allowEntry, bool allowNotifications, Guid? membershipId, Guid? socialGroupId)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Phone = phone;
            Email = email;
            AllowEntry = allowEntry;
            AllowNotifications = allowNotifications;
            MembershipId = membershipId;
            SocialGroupId = socialGroupId;
            CreatedDate = DateTime.UtcNow;
        }

        private Client(Guid id, FullName fullName, string? phone, string email, bool allowEntry, bool allowNotifications, Guid? membershipId, Guid? socialGroupId)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
            Email = email;
            AllowEntry = allowEntry;
            AllowNotifications = allowNotifications;
            MembershipId = membershipId;
            SocialGroupId = socialGroupId;
            CreatedDate = DateTime.UtcNow;
        }

        public static Client Create(string firstName, string secondName, string? patronymic,
            string? phone, string email, bool allowEntry, bool allowNotifications, Guid? membershipId, Guid? socialGroupId)
        {
            var fullName = FullName.Create(firstName, secondName, patronymic);

            if (phone != null && !_phonePattern.IsMatch(phone))
                throw new DomainException($"Invalid value for Client[phone]. Entered value {phone}");
            if (string.IsNullOrWhiteSpace(email) || !_emailPattern.IsMatch(email))
                throw new DomainException($"Invalid value for Client[email]. Entered value {email}");
            if (membershipId == Guid.Empty)
                throw new DomainException($"Invalid value for Client[membershipId]. Entered value {membershipId}");
            if (socialGroupId == Guid.Empty)
                throw new DomainException($"Invalid value for Client[socialGroupId]. Entered value {socialGroupId}");

            return new(fullName, phone, email, allowEntry, allowNotifications, membershipId, socialGroupId);
        }

        public static Client Create(Guid id, string firstName, string secondName, string? patronymic,
            string? phone, string email, bool allowEntry, bool allowNotifications, Guid? membershipId, Guid? socialGroupId)
        {
            if (id == Guid.Empty)
                throw new DomainException($"Invalid value for Client[id]. Entered value {id}");

            var fullName = FullName.Create(firstName, secondName, patronymic);

            if (phone != null && !_phonePattern.IsMatch(phone))
                throw new DomainException($"Invalid value for Client[phone]. Entered value {phone}");
            if (string.IsNullOrWhiteSpace(email) || !_emailPattern.IsMatch(email))
                throw new DomainException($"Invalid value for Client[email]. Entered value {email}");
            if (membershipId == Guid.Empty)
                throw new DomainException($"Invalid value for Client[membershipId]. Entered value {membershipId}");
            if (socialGroupId == Guid.Empty)
                throw new DomainException($"Invalid value for Client[socialGroupId]. Entered value {socialGroupId}");

            return new(id, fullName, phone, email, allowEntry, allowNotifications, membershipId, socialGroupId);
        }

        public void UpdateDetails(string firstName, string secondName, string? patronymic,
            string? phone, string email, bool allowEntry, bool allowNotifications, Guid? membershipId, Guid? socialGroupId)
        {
            var fullName = FullName.Create(firstName, secondName, patronymic);

            if (phone != null && !_phonePattern.IsMatch(phone))
                throw new DomainException($"Invalid value for Client[phone]. Entered value {phone}");
            if (string.IsNullOrWhiteSpace(email) || !_emailPattern.IsMatch(email))
                throw new DomainException($"Invalid value for Client[email]. Entered value {email}");
            if (membershipId == Guid.Empty)
                throw new DomainException($"Invalid value for Client[membershipId]. Entered value {membershipId}");
            if (socialGroupId == Guid.Empty)
                throw new DomainException($"Invalid value for Client[socialGroupId]. Entered value {socialGroupId}");

            FullName = fullName;
            Phone = phone;
            Email = email;
            AllowEntry = allowEntry;
            AllowNotifications = allowNotifications;
            MembershipId = membershipId;
            SocialGroupId = SocialGroupId;
        }
    }
}
