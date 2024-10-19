using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ESys.Application.Contracts.Persistence;
using ESys.Persistence.Dapper.Repositories;
using ESys.Application.Features;
using ESys.Application.Services.FileUploadHandler;
using ESys.Persistence.FileSystem;

namespace ESys.Persistence;
/// <summary>
/// Extension methods needed for adding persistence layer services
/// </summary>
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBizRepository, BizRepository>();
        services.AddScoped<IBizInitialUiRepository, BizInitialUiRepository>();
        services.AddScoped<IBizXmlRepository, BizXmlRepository>();
        services.AddScoped<IFileUploadHandlerService, FileUploadHandlerService>();
        
        return services;
    }

}
