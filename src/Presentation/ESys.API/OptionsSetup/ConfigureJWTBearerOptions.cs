using System.Security.Claims;
using System.Text;
using ESys.Libraries.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ESys.API.OptionsSetup;

public class ConfigureJWTBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger _logger;

    public ConfigureJWTBearerOptions(IOptions<JwtOptions> jwtOptions, ILogger<ConfigureJWTBearerOptions> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jwtOptions = jwtOptions?.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
    }

    public void Configure(JwtBearerOptions options)
    {
        // This method will NOT be called.
        _logger.LogInformation("No name Configure called of {className}", nameof(ConfigureJWTBearerOptions));
        Configure(JwtBearerDefaults.AuthenticationScheme, options);
    }

    public void Configure(string name, JwtBearerOptions options)
    {
        _logger.LogInformation("{name}.{methodName} is called. Policy name: {policyName}",
            nameof(ConfigureJWTBearerOptions), nameof(Configure), name);
        
        options.Authority = _jwtOptions.Authority;
        
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = async (context) =>
            {
                Console.WriteLine("Printing in the delegate OnAuthFailed");
            },
            OnChallenge = async (context) =>
            {
                Console.WriteLine("Printing in the delegate OnChallenge");

                // this is a default method
                // the response statusCode and headers are set here
                context.HandleResponse();

                // AuthenticateFailure property contains 
                // the details about why the authentication has failed
                if (context.AuthenticateFailure != null)
                {
                    context.Response.StatusCode = 401;

                    // we can write our own custom response content here
                    await context.HttpContext.Response.WriteAsync("Token Validation Has Failed. Request Access Denied");
                }
            }
        };
        
        options.TokenValidationParameters = new TokenValidationParameters
        { 
            ValidIssuer = _jwtOptions.Issuer,
            ValidateIssuer = true,

            ValidAudience = _jwtOptions.Audience,
            ValidateAudience = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            ValidateIssuerSigningKey = true,
            
            ValidateLifetime = true,
            
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role,
        };
    }
}