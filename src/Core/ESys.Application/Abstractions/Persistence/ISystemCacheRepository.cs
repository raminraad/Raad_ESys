using ESys.Application.Models;
using ESys.Persistence.Dapper.Repositories;

namespace ESys.Application.Abstractions.Persistence;

public interface ISystemCacheRepository
{
    double LastOrder { get; }
    OpenedClientSessionDto AddClientToOpenSessions(NewClientSessionDto newClientSession);
}