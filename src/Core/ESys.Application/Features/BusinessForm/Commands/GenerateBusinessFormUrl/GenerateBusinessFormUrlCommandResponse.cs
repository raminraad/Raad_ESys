namespace ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;

/// <summary>
/// Response being sent back to business in case of redirection validity
/// </summary>
public class GenerateBusinessFormUrlCommandResponse
{
    public required string TempRoute { get; set; }
}