using ESys.Application.CQRS.BusinessForm.Commands.GenerateJwtForBusinessForm;
using ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;

namespace ESys.Application.Abstractions.Services.JWT;

public interface IJwtProvider
{
    string GenerateJwtForCalcForm(GenerateJwtForBusinessFormCommand req);
    string GenerateJwtForRedirectToBusinessForm(RedirectToBusinessFormJwtGenerationDto req);
}