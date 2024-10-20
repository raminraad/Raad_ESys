using ESys.Application.Abstractions.Persistence;
using ESys.Application.Models;

namespace ESys.Persistence.Dapper.Repositories;

public class SystemCacheRepository : ISystemCacheRepository
{
    private int _counter = 0; // implement this counter in db
    public double ValidRedirectionsToBusinessFormCount => ++_counter;
    public Guid AddClientToOpenSessions(OpenClientSessionDto openClientSession)
    {
        return Guid.NewGuid(); // implement in db
    }
}