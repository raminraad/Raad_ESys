using MediatR;

namespace ESys.Application.Features.BusinessForm.Commands.AthorizeBusinessClient;

public record 
    AthorizeBusinessClientCommand() : IRequest<AthorizeBusinessClientResponse>
{
    public required int BusinessId { get; set; }
    public required string BusinessToken { get; set; }
    public required string IpAddress { get; set; }
}