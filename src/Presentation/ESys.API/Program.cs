using ESys.API.Middleware;
using ESys.Persistence;
using FastEndpoints;
using Scalar.AspNetCore;
using ESys.Application;
using ESys.Libraries;

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
builder.Services.AddApplicationServices();
builder.Services.AddLibrariesServices();
builder.Services.AddPersistenceServices(configuration);

#endregion

builder.Services.AddAuthentication().AddJwtBearer();

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
// app.MapControllers();
app.UseFastEndpoints(c => { c.Endpoints.RoutePrefix = "api"; });

app.Run();