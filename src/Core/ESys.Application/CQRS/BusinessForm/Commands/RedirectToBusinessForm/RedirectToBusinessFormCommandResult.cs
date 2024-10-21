namespace ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;

/// <summary>
/// Response being sent back to business in case of redirection validity
/// </summary>
public class RedirectToBusinessFormCommandResult
{
    public required string Url { get; set; }
}