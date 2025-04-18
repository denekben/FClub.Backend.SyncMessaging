﻿using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IMembershipRepository
    {
        Task AddAsync(Membership membership);
        Task<Membership?> GetAsync(Guid id, MembershipIncludes includes);
        Task<Membership?> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
    }

    [Flags]
    public enum MembershipIncludes
    {
        Client = 1,
        Tariff = 2
    }
}
