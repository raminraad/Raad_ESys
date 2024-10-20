using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ESys.Application.Abstractions.Services.JWT;
using ESys.Application.CQRS.JWT.Commands.BusinessForm;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ESys.Libraries.JWT;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateJwtForCalcForm(GenerateJwtForBusinessFormCommand req)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.NameId, "dizmar"), //modified
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new(JwtRegisteredClaimNames.Sub, "Albert"), // todo: change to business holder name
            new("client-session-id", req.ClientSessionId),
            new("total-session-counter", "200"), // todo: replace from db fetch
        };
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            DateTime.UtcNow, // Not before
            DateTime.UtcNow.AddMinutes(10), // Exp time
            signingCredentials);
        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);
        return tokenValue;
    }
}