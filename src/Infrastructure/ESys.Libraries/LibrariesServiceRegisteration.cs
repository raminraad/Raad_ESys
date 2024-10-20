using ESys.Application.Abstractions.Services.BusinessFormCalculation;
using ESys.Application.Abstractions.Services.JsonHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESys.Libraries;

public static class LibrariesServiceRegisteration
{
    public static IServiceCollection AddLibrariesServices(this IServiceCollection services)
    {
        services.AddScoped<IJsonHandler, JsonHandler>();
        services.AddScoped<IExpHelper, ExpHelper>();

        return services;
    }
}