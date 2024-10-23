using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ESys.Application.Abstractions.Services.JWT;
using ESys.Application.Features.BusinessForm.Commands.AthorizeBusinessClient;
using ESys.Application.Features.BusinessForm.Commands.GenerateJwtForBusinessForm;
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
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.NameId, "client-name"), //todo: change to valid value
            new("role", "client"), //todo: change to valid value
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        
            new(JwtRegisteredClaimNames.Sub, "client-name"), // todo: change to business holder name
        };
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            DateTime.UtcNow, // Not before
            DateTime.UtcNow.AddMinutes(120), // Exp time
            signingCredentials);
        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);
        return tokenValue;
        
        
        
        
        
        
        // var claims = new Claim[]
        // {
        //     new("client-session-id", req.ClientSessionId),
        //     new("business-id", req.BusinessId),
        //     new("counter","200")
        // };
        //
        //
        // var jwt = JwtBearer.CreateToken(
        //     o =>
        //     {
        //         o.SigningKey = "A_Secret_Token_Signing_Key_Longer_Than_32_Characters";
        //         o.ExpireAt = DateTime.UtcNow.AddYears(10);
        //     });
        //
        //
        //
        // var token = new JwtSecurityToken(
        //     _options.Issuer,
        //     _options.Audience,
        //     claims,
        //     DateTime.UtcNow, // Not before
        //     DateTime.UtcNow.AddHours(1), // Exp time
        //     signingCredentials);
        // string tokenValue = new JwtSecurityTokenHandler()
        //     .WriteToken(token);
        // return jwt;
    }

    public string GenerateJwtForClient(RequestClientJwtDto req)
    {
        var claims = new Claim[]
        {
            new("bid", req.BusinessId.ToString()),
            new("tr", req.TempRoute.ToString()),
            new("bt", req.BusinessToken),
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
            req.ExpireDateTime, // Exp time
            signingCredentials);
        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);
        return jwt;
    }
}