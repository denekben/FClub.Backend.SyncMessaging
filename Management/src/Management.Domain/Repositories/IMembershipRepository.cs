using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IMembershipRepository
    {
        Task AddAsync(Membership membership);
        Task<Membership?> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Membership membership);
    }
}
