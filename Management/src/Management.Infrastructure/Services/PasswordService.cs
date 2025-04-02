using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Management.Infrastructure.Service
{
    public class PasswordService : IPasswordService
    {
        private readonly int _keySize;
        private readonly int _iterations;
        private readonly HashAlgorithmName _hashAlgorithm;
        private readonly double _refreshTokenLifeTime;
        private static readonly Regex _passwordPattern = new(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,30}$");

        public PasswordService(IConfiguration configuration)
        {
            _hashAlgorithm = HashAlgorithmName.SHA512;
            _keySize = Convert.ToInt32(configuration["Hashing:KeySize"]);
            _iterations = Convert.ToInt32(configuration["Hashing:Iterations"]);
        }

        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || !_passwordPattern.IsMatch(password) || password.Contains(' '))
                throw new DomainException($"Invalid argument for AppUser[password]. Entered value: {password}");

            var salt = RandomNumberGenerator.GetBytes(_keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize
            );

            return $"{Convert.ToHexString(hash)}:{Convert.ToHexString(salt)}";
        }

        public bool IsPasswordValid(string password, string storedHashWithSalt)
        {
            var parts = storedHashWithSalt.Split(':');
            if (parts.Length != 2)
                throw new BadRequestException("Incorect hash format");

            var storedHash = parts[0];
            var storedSalt = parts[1];

            var saltBytes = Convert.FromHexString(storedSalt);
            var hashToCheck = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                _iterations,
                _hashAlgorithm,
                _keySize);

            return storedHash.Equals(Convert.ToHexString(hashToCheck), StringComparison.OrdinalIgnoreCase);
        }
    }
}
