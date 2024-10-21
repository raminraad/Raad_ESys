namespace ESys.Application.Models;

public class NewClientSessionDto
{
    public required string BusinessId { get; init; }
    public required string BusinessToken { get; init; }
    public required string ClientToken { get; init; }
    public required string IpAddress { get; init; }
    public required string ClientSessionId { get; init; }
}