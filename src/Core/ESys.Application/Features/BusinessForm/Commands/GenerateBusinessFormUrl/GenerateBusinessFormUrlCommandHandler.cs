using AutoMapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Application.Abstractions.Services.JWT;
using ESys.Domain.Entities;
using MediatR;

namespace ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;

public sealed class
    GenerateBusinessFormUrlCommandHandler : IRequestHandler<GenerateBusinessFormUrlCommand,
    GenerateBusinessFormUrlCommandResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly IClientSessionCacheRepository _clientSessionCacheRepository;

    public GenerateBusinessFormUrlCommandHandler(IJwtProvider jwtProvider, IMapper mapper,
        IClientSessionCacheRepository clientSessionCacheRepository)
    {
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _clientSessionCacheRepository = clientSessionCacheRepository;
    }

    public async Task<GenerateBusinessFormUrlCommandResponse> Handle(GenerateBusinessFormUrlCommand command,
        CancellationToken cancellationToken)
    {
        // todo: check validity via FluentValidation
        var apiIsValid = true;
        if (apiIsValid is false)
        {
            // todo: create and throw exception
            throw new Exception();
        }


        var tempRoute = Guid.NewGuid();
        string clientToken =
            _jwtProvider.GenerateJwtForClient(new RequestClientJwtDto
            {
                BusinessId = command.BusinessId,
                BusinessToken = command.BusinessToken,
                TempRoute = tempRoute,
                ExpireDateTime = DateTime.UtcNow.AddMinutes(10)
            });

        ClientSessionCache newSessionCache = new()
        {
            TempRoute = tempRoute,
            ClientToken = clientToken,
            BusinessId = command.BusinessId, 
        };
        
        newSessionCache = await  _clientSessionCacheRepository.Add(newSessionCache);
        var response = new GenerateBusinessFormUrlCommandResponse
        {
            TempRoute = newSessionCache.TempRoute.ToString()
        };
        return response;
    }
}