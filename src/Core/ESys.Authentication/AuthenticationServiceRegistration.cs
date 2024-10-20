using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ESys.Application.Abstractions.Services.FileUpload;
using ESys.Application.CQRS.BusinessForm;

namespace ESys.Authentication;

/// <summary>
/// Extension methods needed for adding authentication layer services
/// </summary>
public static class AuthenticationServiceRegistration
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped<FileUploadConfigDto,FileUploadConfigDto>();
        services.AddScoped<BusinessFormCalculator>();
        services.AddScoped<BusinessFormInitiator>();

        return services;
    }
}

