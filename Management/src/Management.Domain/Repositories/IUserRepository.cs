using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task<AppUser?> GetUserByEmailAsync(string email);
        Task<AppUser?> GetUserByEmailAsync(string email, UserIncludes includes);
        Task<bool> IsBlockedAsync(Guid id);
        Task AddAsync(AppUser user);
        Task UpdateAsync(AppUser user);
        Task<AppUser?> GetAsync(Guid id);
        Task<AppUser?> GetAsync(Guid id, UserIncludes includes);
    }

    [Flags]
    public enum UserIncludes
    {
        Role = 1
    }
}
