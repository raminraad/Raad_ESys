using MediatR;

namespace ESys.Application.Features.BusinessForm.Commands.GenerateBusinessFormUrl;

public record 
    GenerateBusinessFormUrlCommand() : IRequest<GenerateBusinessFormUrlCommandResponse>
{
    public required int BusinessId { get; set; }
    public required string BusinessToken { get; set; }
    public required string IpAddress { get; set; }
}