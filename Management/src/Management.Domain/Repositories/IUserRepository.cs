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
        Task SaveChangesAsync();
        Task<AppUser?> GetAsync(Guid id);
    }
}
