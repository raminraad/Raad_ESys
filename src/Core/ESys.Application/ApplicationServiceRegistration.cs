using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ESys.Application.Abstractions.Services.FileUploadHandler;
using ESys.Application.CQRS.BusinessForm;

namespace ESys.Application;

/// <summary>
/// Extension methods needed for adding application layer services
/// </summary>
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped<FileUploadConfigDto,FileUploadConfigDto>();
        services.AddScoped<BusinessFormCalculator>();
        services.AddScoped<BusinessFormInitiator>();

        return services;
    }
}

