using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> AllExists(List<Guid> id);
    }
}
