using ESys.Authentication.JWT.Commands.BusinessForm;

namespace ESys.Authentication.Services;

public interface IJwtProvider
{
    string GenerateJwtForCalcForm(GenerateJwtForBusinessFormCommand req);
}