using CSharpFunctionalExtensions;
using ESys.Application.Abstractions.CQRS;
using ESys.Authentication.Services;
using ESys.Authentication.SharedKernel;

namespace ESys.Authentication.JWT.Commands.BusinessForm;

public sealed class
    GenerateJwtForBusinessFormCommandHandler : ICommandHandler<GenerateJwtForBusinessFormCommand, string>
{
    private readonly IJwtProvider _jwtProvider;

    public GenerateJwtForBusinessFormCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(GenerateJwtForBusinessFormCommand command, CancellationToken cancellationToken)
    {
        // todo: check for API validity through repository
        var apiIsValid = true;
        if (apiIsValid is false)
        {
            return Result.Failure<string>(AuthenticationErrors.BusinessTokenHasBeenExpired).Value;
        }

        string token = _jwtProvider.GenerateJwtForCalcForm(command);

        return token;
    }

}