using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IUserLogRepository
    {
        Task AddAsync(UserLog log);
    }
}
