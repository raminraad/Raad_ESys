using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ESys.Application.Contracts.Persistence;
using ESys.Application.Contracts.Services.FileUploadHandler;
using ESys.Persistence.Dapper.Repositories;
using ESys.Application.Features;
using ESys.Persistence.FileSystem;

namespace ESys.Persistence;
/// <summary>
/// Extension methods needed for adding persistence layer services
/// </summary>
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IBusinessInitialUiRepository, BusinessInitialUiRepository>();
        services.AddScoped<IBusinessXmlRepository, BusinessXmlRepository>();
        services.AddScoped<IFileUploadHandlerService, FileUploadHandlerService>();
        
        return services;
    }

}
