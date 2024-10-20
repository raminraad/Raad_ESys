using ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;
using ESys.Application.CQRS.JWT.Commands.BusinessForm;

namespace ESys.Application.Abstractions.Services.JWT;

public interface IJwtProvider
{
    string GenerateJwtForCalcForm(GenerateJwtForBusinessFormCommand req);
    string GenerateJwtForRedirectToBusinessForm(RedirectToBusinessFormJwtGenerationDto req);
}