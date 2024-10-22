using ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;
using ESys.Application.Features.BusinessForm.Commands.GenerateJwtForBusinessForm;

namespace ESys.Application.Abstractions.Services.JWT;

public interface IJwtProvider
{
    string GenerateJwtForCalcForm(GenerateJwtForBusinessFormCommand req);
    string GenerateJwtForRedirectToBusinessForm(RequestClientJwtDto req);
}