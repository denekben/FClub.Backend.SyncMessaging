using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task<AppUser?> GetUserByEmailAsync(string email);
        Task<bool> IsBlockedAsync(Guid id);
        Task AddAsync(AppUser user);
        Task UpdateAsync(AppUser user);
        Task<AppUser?> GetAsync(Guid id);
        Task SaveChangesAsync();
    }
}
