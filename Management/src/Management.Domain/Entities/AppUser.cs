using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.ValueObjects;
using System.Text.RegularExpressions;

namespace Management.Domain.Entities
{
    public sealed class AppUser
    {
        private readonly static Regex _phonePattern = new(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", RegexOptions.IgnoreCase);
        private readonly static Regex _emailPattern = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);

        public Guid Id { get; init; }
        public FullName FullName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsBlocked { get; set; }
        public bool AllowEntry { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public DateTime CreatedDate { get; init; }
        public DateTime? UpdatedDate { get; set; }

        private AppUser() { }

        private AppUser(FullName fullName, string? phone,
            string email, string passwordHash, bool isBlocked, bool allowEntry,
            string? refreshToken, DateTime refreshTokenExpires, Guid roleId)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Phone = phone;
            Email = email;
            PasswordHash = passwordHash;
            IsBlocked = isBlocked;
            AllowEntry = allowEntry;
            RefreshToken = refreshToken;
            RefreshTokenExpires = refreshTokenExpires;
            RoleId = roleId;
            CreatedDate = DateTime.UtcNow;
        }

        public static AppUser Create(string firstName, string secondName, string? patronymic, string? phone,
            string email, string passwordHash, bool isBlocked, bool allowEntry,
            string? refreshToken, DateTime refreshTokenExpires, Guid roleId)
        {
            var fullName = FullName.Create(firstName, secondName, patronymic);
            if (phone != null && !_phonePattern.IsMatch(phone))
                throw new DomainException($"Invalid value for AppUser[phone]. Entered value {phone}");
            if (string.IsNullOrWhiteSpace(email) || !_emailPattern.IsMatch(email))
                throw new DomainException($"Invalid value for AppUser[email]. Entered value {email}");
            if (roleId == Guid.Empty)
                throw new DomainException($"Invalid value for AppUser[roleId]. Entered value {roleId}");
            return new(fullName, phone, email, passwordHash,
                isBlocked, allowEntry, refreshToken, refreshTokenExpires, roleId);
        }
    }
}
