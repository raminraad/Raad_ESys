using System.Security.Claims;
using System.Text;
using ESys.API.Middleware;
using ESys.API.OptionsSetup;
using ESys.Persistence;
using FastEndpoints;
using Scalar.AspNetCore;
using ESys.Application;
using ESys.Libraries;
using ESys.API.Profiles.AutoMappers;
using ESys.Application.Abstractions.Services.BusinessForm;
using ESys.Libraries.JWT;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


#region Development Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("ESysCorsPolicy", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5000",
                "https://localhost:5000",
                "http://localhost:3000",
                "https://localhost:3000",
                "http://localhost:5001",
                "https://localhost:5001",
                "http://localhost:5005",
                "https://localhost:5006",
                $"http://{Environment.GetEnvironmentVariable("DEPLOY_CDN")}",
                $"https://{Environment.GetEnvironmentVariable("DEPLOY_CDN")}"
            )
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

#endregion

builder.Services.AddFastEndpoints();
// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Add referenced projects services

var configuration = new ConfigurationBuilder().Build();
builder.Services.AddEPaaSApplication();
builder.Services.AddEPaasServices();
builder.Services.AddEPaaSPersistence(configuration);

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(GenerateBusinessFormUrlMapper));

#endregion



#region Jwt

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.AddTransient<IConfigureOptions<JwtBearerOptions>, ConfigureJWTBearerOptions>();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
    options.AddPolicy("client", p => p.RequireRole("client"));
});

#endregion

// JWT Configuration which works fine

#region new jwt

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.Authority = "https://localhost:5006";
//         options.RequireHttpsMetadata = false;
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = "issuer",
//             ValidAudience = ("audience"),
//             ValidateLifetime = true,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(("ThisIsMySecretKeyWhichShouldBeReplacedWithAComplexOne")!)),
//             ClockSkew = TimeSpan.Zero,
//             ValidateAudience = true,
//             ValidateIssuer = true,
//             IgnoreTrailingSlashWhenValidatingAudience = true,
//             
//         };
//     });


// builder.Services.AddAuthorization(options =>
// {
//     options.DefaultPolicy = new AuthorizationPolicyBuilder()
//         .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//         .RequireAuthenticatedUser()
//         .Build();
//
// });
#endregion

builder.Services.AddTransient<BusinessFormCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { options.RouteTemplate = "openapi/{documentName}.json"; });
    app.MapScalarApiReference();
    // app.UseSwaggerUI();
    app.UseCors("ESysCorsPolicy");
}

// var claims = new List<Claim>();
// claims.Add(new Claim("usr","anton"));
// var identity = new ClaimsIdentity(claims,"bearer");
// var user = new ClaimsPrincipal(identity);

 

app.UseHttpsRedirection();
// app.UseCustomExceptionHandler();
app.UseHsts();

// app.UseCors();
app.UseFastEndpoints(c => { c.Endpoints.RoutePrefix = "api";});
app.UseAuthentication();
app.UseAuthorization();
// app.UseCustomJwtMiddleware();
// app.MapControllers();

app.Run();