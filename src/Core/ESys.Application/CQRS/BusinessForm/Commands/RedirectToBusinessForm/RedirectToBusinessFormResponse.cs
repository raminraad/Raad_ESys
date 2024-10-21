namespace ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;

/// <summary>
/// Response being sent back to business in case of redirection validity
/// </summary>
public class RedirectToBusinessFormResponse
{
    public required string BackUrl { get; set; }
}