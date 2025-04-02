using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string name);
        Task<Role?> GetAsync(Guid id);
    }
}
