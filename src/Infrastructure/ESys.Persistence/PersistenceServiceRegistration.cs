using ESys.Application.Abstractions.Persistence;
using ESys.Application.Abstractions.Services.FileUpload;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ESys.Persistence.Dapper.Repositories;
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
        services.AddScoped<IFileUploadService, FileUploadService>();
        
        return services;
    }

}
