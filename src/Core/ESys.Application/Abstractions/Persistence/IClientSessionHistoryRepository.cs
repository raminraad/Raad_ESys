using ESys.Domain.Entities;

namespace ESys.Application.Abstractions.Persistence;

public interface IClientSessionHistoryRepository : IAsyncRepository<ClientSessionHistory>
{
    Task<ClientSessionHistory?> GetByTempRoute(Guid tempRoute);
}