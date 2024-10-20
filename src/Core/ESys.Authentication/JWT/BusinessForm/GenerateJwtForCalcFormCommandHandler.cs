using CommandQuery;
using CSharpFunctionalExtensions;
using ESys.Authentication.Services;
using ESys.Authentication.SharedKernel;

namespace ESys.Authentication.JWT.BusinessForm;

public sealed class
    GenerateJwtForCalcFormCommandHandler : ICommandHandler<GenerateJwtForCalcFormCommand, string>
{
    private readonly IJwtProvider _jwtProvider;

    public GenerateJwtForCalcFormCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public async Task<string> HandleAsync(GenerateJwtForCalcFormCommand command, CancellationToken cancellationToken)
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