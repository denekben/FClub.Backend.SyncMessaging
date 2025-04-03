using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface ITariffRepository
    {
        Task<Tariff?> GetAsync(Guid id);
        Task AddAsync(Tariff tariff);
        Task UpdateAsync(Tariff tariff);
        Task DeleteAsync(Guid id);
    }
}
