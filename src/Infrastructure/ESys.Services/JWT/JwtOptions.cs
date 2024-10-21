namespace ESys.Libraries.JWT;

public sealed record JwtOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; }
    
}