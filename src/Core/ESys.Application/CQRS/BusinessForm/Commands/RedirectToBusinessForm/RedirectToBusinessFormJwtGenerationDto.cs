namespace ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;

public class RedirectToBusinessFormJwtGenerationDto
{
    public required string BusinessId { get; set; }
    public required string ClientSessionId { get; set; }
    public required string Counter { get; set; }
}