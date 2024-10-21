using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ESys.Application.Abstractions.Services.JWT;
using ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;
using ESys.Application.CQRS.JWT.Commands.BusinessForm;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ESys.Libraries.JWT;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly SigningCredentials signingCredentials;
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
        signingCredentials  = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
    }

    public string GenerateJwtForCalcForm(GenerateJwtForBusinessFormCommand req)
    {
        // var claims = new Claim[]
        // {
        //     new(JwtRegisteredClaimNames.NameId, "my-name"), //todo: change to valid value
        //     new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //
        //     new(JwtRegisteredClaimNames.Sub, "Albert"), // todo: change to business holder name
        //     new("client-session-id", req.ClientSessionId),
        //     new("total-session-counter", "200"), // todo: replace from db fetch
        // };
        // var token = new JwtSecurityToken(
        //     _options.Issuer,
        //     _options.Audience,
        //     claims,
        //     DateTime.UtcNow, // Not before
        //     DateTime.UtcNow.AddHours(10), // Exp time
        //     signingCredentials);
        // string tokenValue = new JwtSecurityTokenHandler()
        //     .WriteToken(token);
        // return tokenValue;
        var claims = new Claim[]
        {
            new("client-session-id", req.ClientSessionId),
            new("business-id", req.BusinessId),
            new("counter","200")
        };

        
        var jwt = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = "A_Secret_Token_Signing_Key_Longer_Than_32_Characters";
                o.ExpireAt = DateTime.UtcNow.AddYears(10);
            });

        
        
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            DateTime.UtcNow, // Not before
            DateTime.UtcNow.AddHours(1), // Exp time
            signingCredentials);
        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);
        return jwt;
    }

    public string GenerateJwtForRedirectToBusinessForm(RedirectToBusinessFormJwtGenerationDto req)
    {
        var claims = new Claim[]
        {
            new("client-session-id", req.ClientSessionId),
            new("business-id", req.BusinessId),
            new("counter", req.Counter)
        };

        
        var jwt = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = "A_Secret_Token_Signing_Key_Longer_Than_32_Characters";
                o.ExpireAt = DateTime.UtcNow.AddYears(10);
            });

        
        
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            DateTime.UtcNow, // Not before
            DateTime.UtcNow.AddHours(1), // Exp time
            signingCredentials);
        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);
        return jwt;
    }
}