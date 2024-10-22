namespace ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;

public class RequestClientJwtDto
{
    public required int BusinessId { get; init; }
    public required string BusinessToken { get; init; }
    public required Guid TempRoute { get; init; }
    public required DateTime ExpireDateTime { get; init; }
}