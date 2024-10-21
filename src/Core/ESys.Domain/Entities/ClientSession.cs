namespace ESys.Domain.Entities;

public class ClientSession
{
    public Guid SessionId { init; get; }
    public required double Order { init; get; }
    public required bool IsOpen { set; get; } = false;
    public required string Url { get; set; }
    public required string ClientToken { get; set; }
}