using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ESys.Application.Contracts.Persistence;
using ESys.Application.Contracts.Services.FileUploadHandler;
using ESys.Application.Features.BusinessForm;

namespace ESys.Application;

/// <summary>
/// Extension methods needed for adding application layer services
/// </summary>
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped<FileUploadHandlerConfig,FileUploadHandlerConfig>();
        services.AddScoped<BusinessFormCalculator>();
        services.AddScoped<BusinessFormInitiator>();

        return services;
    }
}

