﻿using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        public Task AddAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Client?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
