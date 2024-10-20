namespace ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;

/// <summary>
/// Response being sent back to business in case of redirection validity
/// </summary>
public class RedirectToBusinessFormResponse
{
    public Guid UniqueId { get; set; }
    public required string BackUrlBase { get; set; }
    public required string ClientToken { get; set; }
}