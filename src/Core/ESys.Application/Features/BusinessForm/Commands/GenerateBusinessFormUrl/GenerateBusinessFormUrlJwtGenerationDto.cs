namespace ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;

public class GenerateBusinessFormUrlJwtGenerationDto
{
    public required string BusinessId { get; set; }
    public required string ClientSessionId { get; set; }
    public required string Counter { get; set; }
}