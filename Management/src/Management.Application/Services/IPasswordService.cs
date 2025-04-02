namespace Management.Application.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool IsPasswordValid(string password, string storedHashWithSalt);
    }
}
