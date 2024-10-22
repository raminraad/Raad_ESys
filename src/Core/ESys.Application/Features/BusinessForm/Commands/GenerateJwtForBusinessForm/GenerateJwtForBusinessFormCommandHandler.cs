using ESys.Application.Abstractions.Persistence;
using ESys.Application.Abstractions.Services.JWT;
using ESys.Application.SharedKernel;
using MediatR;

namespace ESys.Application.Features.BusinessForm.Commands.GenerateJwtForBusinessForm;

public class
    GenerateJwtForBusinessFormCommandHandler : IRequestHandler<GenerateJwtForBusinessFormCommand, string>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IClientSessionCacheRepository _clientSessionCacheRepository;

    public GenerateJwtForBusinessFormCommandHandler(IJwtProvider jwtProvider,IClientSessionCacheRepository clientSessionCacheRepository)
    {
        _jwtProvider = jwtProvider;
        _clientSessionCacheRepository = clientSessionCacheRepository;
    }

    public Task<string> Handle(GenerateJwtForBusinessFormCommand command, CancellationToken cancellationToken)
    {
        // todo: check for API validity through repository
        var apiIsValid = true;
        if (apiIsValid is false)
        {
            // todo: create and throw exception
            return Task.FromResult(AuthenticationErrors.BusinessTokenHasBeenExpired);
        }

        string token = _jwtProvider.GenerateJwtForCalcForm(command);

        
        
        return Task.FromResult(token);
    }

}