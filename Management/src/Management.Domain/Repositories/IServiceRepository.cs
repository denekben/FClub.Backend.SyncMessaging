using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetAsync(Guid id);
        Task<Service?> GetByNameAsync(string name);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> AllExistsAsync(List<Guid> id);
        Task<bool> AllExistsByNameAsync(List<string> serviceNames);
        Task DeleteOneBranchServicesAsync(List<Guid> serviceIds);
        Task DeleteOneBranchServicesByNameAsync(List<string> servicesName, Guid branchId);
        Task DeleteAsync(Guid id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
    }
}
