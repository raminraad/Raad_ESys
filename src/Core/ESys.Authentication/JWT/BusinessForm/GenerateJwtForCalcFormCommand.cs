using CommandQuery;

namespace ESys.Authentication.JWT.BusinessForm;

public record GenerateJwtForCalcFormCommand(string Email) : ICommand<string>
{
    
}