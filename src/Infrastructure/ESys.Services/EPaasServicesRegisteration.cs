using ESys.Application.Abstractions.Services.BusinessFormCalculation;
using ESys.Application.Abstractions.Services.JSON;
using ESys.Application.Abstractions.Services.JWT;
using ESys.Libraries.BusinessForm;
using ESys.Libraries.JSON;
using ESys.Libraries.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESys.Libraries;

public static class EPaasServicesRegisteration
{
    public static IServiceCollection AddEPaasServices(this IServiceCollection services)
    {
        services.AddScoped<IJsonHandler, JsonHandler>();
        services.AddScoped<IExpHandler, ExpHandler>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}