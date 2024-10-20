using ESys.Application.Models;

namespace ESys.Application.Abstractions.Persistence;

public interface ISystemCacheRepository
{
    double ValidRedirectionsToBusinessFormCount { get; }
    Guid AddClientToOpenSessions(OpenClientSessionDto openClientSession);
}