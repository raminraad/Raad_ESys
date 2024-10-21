using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ESys.Application.Abstractions.Services.BusinessForm;
using ESys.Application.Abstractions.Services.FileUpload;
using ESys.Application.Profiles.AutoMappers;

namespace ESys.Application;

/// <summary>
/// Extension methods needed for adding application layer services
/// </summary>
public static class EPaaSApplicationRegistration
{
    public static IServiceCollection AddEPaaSApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        #region AutoMapper

        services.AddAutoMapper(typeof(BusinessFormDtoMapper));

        #endregion
        
        services.AddScoped<FileUploadConfigDto>();
        services.AddScoped<BusinessFormCalculator>();
        services.AddScoped<BusinessFormInitiator>();

        return services;
    }
}

