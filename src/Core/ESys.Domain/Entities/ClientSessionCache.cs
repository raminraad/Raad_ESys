namespace ESys.Domain.Entities;

public class ClientSessionCache
{
    public uint ClientSessionCacheId { get; init; }
    public int BusinessId { get; set; }
    public Guid TempRoute { init; get; }
    public required string ClientToken { get; set; }
    public Business? Business { get; set; } = default;

}