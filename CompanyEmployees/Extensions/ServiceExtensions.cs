﻿using CompanyEmployees.Presentation.ActionFilters;
using Contract.Interfaces;
using LoggerService.Services;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts.Interfaces;

namespace CompanyEmployees.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
        });
    }
    public static void ConfigureIISIntegration(this IServiceCollection services)
    {
        services.Configure<IISOptions>(options =>
        {
            //TODO
        });
    }
    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }
    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
    }
    public static void ConfigureFilterAttribute(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
    }
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }
}