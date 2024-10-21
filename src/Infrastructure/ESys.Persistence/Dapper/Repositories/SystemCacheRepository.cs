using AutoMapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Application.Models;
using ESys.Persistence.Static;

namespace ESys.Persistence.Dapper.Repositories;

public class SystemCacheRepository : ISystemCacheRepository
{
    private readonly IMapper _mapper;
    private int _order = 0; // implement this counter in db
    private List<ClientSession> _sessions = [];
    public double LastOrder => _order;

    public SystemCacheRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    public OpenedClientSessionDto AddClientToOpenSessions(NewClientSessionDto newClientSession)
    {
        var sessionId = Guid.NewGuid();
        ClientSession newSession = new()
        {
            SessionId = sessionId,
            Order = ++_order,
            ClientToken = newClientSession.ClientToken,
            IsOpen = true,
            Url = $"{ SystemStatics.BusinessFormBaseUrl } + {sessionId}",
        };
        _sessions.Add(newSession);


        return _mapper.Map<OpenedClientSessionDto>(newSession);
    }
}