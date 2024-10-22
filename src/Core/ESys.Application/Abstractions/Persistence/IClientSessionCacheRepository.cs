using ESys.Domain.Entities;

namespace ESys.Application.Abstractions.Persistence;

public interface IClientSessionCacheRepository : IAsyncRepository<ClientSessionCache>
{
    Task<ClientSessionCache?> GetByTempRoute(Guid tempRoute);
}