using AutoMapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Application.Abstractions.Services.JSON;
using ESys.Application.Models;
using ESys.Domain.Entities;
using ESys.Persistence.Static;

namespace ESys.Persistence.Dapper.Repositories;

public class SystemCacheRepository : ISystemCacheRepository
{
    private readonly IMapper _mapper;
    private readonly IJsonHandler _jsonHandler;
    private int _order = 0; // implement this counter in db
    private List<ClientSession> _sessions = [];
    public double LastOrder => _order;

    public SystemCacheRepository(IMapper mapper,IJsonHandler jsonHandler)
    {
        _mapper = mapper;
        _jsonHandler = jsonHandler;
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
            Url = $"{ SystemStatics.BusinessFormBaseUrl }/{sessionId}",
        };
        _sessions.Add(newSession);

        var opensessions = new List<ClientSession>();
        try
        {
             opensessions = _jsonHandler.SimpleRead<List<ClientSession>>($"D:\\f.j");
        }
        catch (Exception e)
        {
            
        }
        opensessions.Add(newSession);
        _jsonHandler.SimpleWrite(opensessions,$"D:\\f.j");
        return _mapper.Map<OpenedClientSessionDto>(newSession);
    }
}