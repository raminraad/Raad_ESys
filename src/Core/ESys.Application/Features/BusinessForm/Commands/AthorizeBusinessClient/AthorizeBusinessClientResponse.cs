namespace ESys.Application.Features.BusinessForm.Commands.AthorizeBusinessClient;

/// <summary>
/// Response being sent back to business in case of redirection validity
/// </summary>
public class AthorizeBusinessClientResponse
{
    public required string TempRoute { get; set; }
}