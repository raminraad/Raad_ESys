using AutoMapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Application.Abstractions.Services.JWT;
using ESys.Application.Models;
using MediatR;

namespace ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;

public sealed class
    GenerateBusinessFormUrlCommandHandler : IRequestHandler<GenerateBusinessFormUrlCommand, GenerateBusinessFormUrlCommandResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly ISystemCacheRepository _systemCacheRepository;

    public GenerateBusinessFormUrlCommandHandler(IJwtProvider jwtProvider,IMapper mapper,ISystemCacheRepository systemCacheRepository)
    {
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _systemCacheRepository = systemCacheRepository;
    }

    public Task<GenerateBusinessFormUrlCommandResponse> Handle(GenerateBusinessFormUrlCommand urlCommand, CancellationToken cancellationToken)
    {
        // todo: check validity via FluentValidation
        var apiIsValid = true;
        if (apiIsValid is false)
        {
            // todo: create and throw exception
            throw new Exception();
        }

        var jwtDto = _mapper.Map<GenerateBusinessFormUrlJwtGenerationDto>(urlCommand);
        string token = _jwtProvider.GenerateJwtForRedirectToBusinessForm(jwtDto);
        
        var newSession = _systemCacheRepository.AddClientToOpenSessions(_mapper.Map<NewClientSessionDto>(urlCommand));
        var response = new GenerateBusinessFormUrlCommandResponse
        {
            Url = newSession.Url,
        };
        return Task.FromResult(response);
    }

}