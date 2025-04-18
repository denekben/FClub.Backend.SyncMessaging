﻿using AccessControll.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task AddAsync(Service service);
        Task<List<Service>?> GetByBranchId(Guid id);
        Task<List<Service>?> GetByTariffId(Guid id);
    }
}
