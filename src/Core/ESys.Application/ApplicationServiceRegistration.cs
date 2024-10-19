﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ESys.Application.Contracts.Persistence;
using ESys.Application.Features.BizForm;
using ESys.Application.Services.FileUploadHandler;

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
        services.AddScoped<BizFormCalculator>();
        services.AddScoped<BizFormInitiator>();

        return services;
    }
}

