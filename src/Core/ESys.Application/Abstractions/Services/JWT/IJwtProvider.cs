using ESys.Application.CQRS.JWT.Commands.BusinessForm;

namespace ESys.Application.Abstractions.Services.JWT;

public interface IJwtProvider
{
    string GenerateJwtForCalcForm(GenerateJwtForBusinessFormCommand req);
}