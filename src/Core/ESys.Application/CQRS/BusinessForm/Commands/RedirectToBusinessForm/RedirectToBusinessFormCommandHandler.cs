using AutoMapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Application.Abstractions.Services.JWT;
using ESys.Application.Models;
using ESys.Application.Statics;
using MediatR;

namespace ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;

public sealed class
    RedirectToBusinessFormCommandHandler : IRequestHandler<RedirectToBusinessFormCommand, RedirectToBusinessFormResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly ISystemCacheRepository _systemCacheRepository;

    public RedirectToBusinessFormCommandHandler(IJwtProvider jwtProvider,IMapper mapper,ISystemCacheRepository systemCacheRepository)
    {
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _systemCacheRepository = systemCacheRepository;
    }

    public Task<RedirectToBusinessFormResponse> Handle(RedirectToBusinessFormCommand command, CancellationToken cancellationToken)
    {
        // todo: check validity via FluentValidation
        var apiIsValid = true;
        if (apiIsValid is false)
        {
            // todo: create and throw exception
            throw new Exception();
        }

        var jwtDto = _mapper.Map<RedirectToBusinessFormJwtGenerationDto>(command);
        string token = _jwtProvider.GenerateJwtForRedirectToBusinessForm(jwtDto);
        var response = new RedirectToBusinessFormResponse
        {
            BackUrlBase = BusinessFormStatics.BusinessFormUrlBase,
            ClientToken = token
        };
        Guid uniqueId = _systemCacheRepository.AddClientToOpenSessions(_mapper.Map<OpenClientSessionDto>(response));
        response.UniqueId = uniqueId;
        return Task.FromResult(response);
    }

}