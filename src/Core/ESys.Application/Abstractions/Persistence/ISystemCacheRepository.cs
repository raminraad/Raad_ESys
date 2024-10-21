using ESys.Application.Models;

namespace ESys.Application.Abstractions.Persistence;

public interface ISystemCacheRepository
{
    double LastOrder { get; }
    OpenedClientSessionDto AddClientToOpenSessions(NewClientSessionDto newClientSession);
}