using ESys.Authentication.JWT.BusinessForm;

namespace ESys.Authentication.Services;

public interface IJwtProvider
{
    string GenerateJwtForCalcForm(GenerateJwtForCalcFormCommand req);
}