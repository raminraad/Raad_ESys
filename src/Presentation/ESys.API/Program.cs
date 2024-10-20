using ESys.API.Middleware;
using ESys.API.OptionsSetup;
using ESys.Persistence;
using FastEndpoints;
using Scalar.AspNetCore;
using ESys.Application;
using ESys.Libraries;
using ESys.API.Profiles.AutoMappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();

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


builder.Services.AddAuthorization();
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

builder.Services.AddAutoMapper(typeof(GenerateJwtForBusinessFormMapper));

#endregion

#region Jwt

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
// builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = "This-is-a-complicated-secret-key-for-EPaad-JWT-!@65#5$#6$%%5_^*1(0^&*(%_^6541&#$@!@_#55$321!@");

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { options.RouteTemplate = "openapi/{documentName}.json"; });
    app.MapScalarApiReference();
    // app.UseSwaggerUI();
    app.UseCors("ESysCorsPolicy");
}

app.UseHttpsRedirection();
app.UseCustomExceptionHandler();
app.UseHsts();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
// app.UseCustomJwtMiddleware();
// app.MapControllers();
app.UseFastEndpoints(c => { c.Endpoints.RoutePrefix = "api"; });

app.Run();