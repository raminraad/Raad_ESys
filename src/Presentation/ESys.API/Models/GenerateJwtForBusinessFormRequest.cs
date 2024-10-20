namespace ESys.API.Models;

public record GenerateJwtForBusinessFormRequest()
{
    public required string Email { get; set; }
}