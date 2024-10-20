using ESys.Application.Abstractions.Services.JWT;
using ESys.Application.SharedKernel;
using MediatR;

namespace ESys.Application.CQRS.JWT.Commands.BusinessForm;

public sealed class
    GenerateJwtForBusinessFormCommandHandler : IRequestHandler<GenerateJwtForBusinessFormCommand, string>
{
    private readonly IJwtProvider _jwtProvider;

    public GenerateJwtForBusinessFormCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public Task<string> Handle(GenerateJwtForBusinessFormCommand command, CancellationToken cancellationToken)
    {
        // todo: check for API validity through repository
        var apiIsValid = true;
        if (apiIsValid is false)
        {
            return Task.FromResult(AuthenticationErrors.BusinessTokenHasBeenExpired);
        }

        string token = _jwtProvider.GenerateJwtForCalcForm(command);

        return Task.FromResult(token);
    }

}