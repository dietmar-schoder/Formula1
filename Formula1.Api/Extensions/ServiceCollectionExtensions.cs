﻿using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.ExternalServices;
using Formula1.Domain.Common.Interfaces;
using Formula1.Infrastructure.ExternalApis;
using Formula1.Infrastructure.Services;

namespace Formula1.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        var isDevelopment = environment.IsDevelopment();
        services.AddHttpContextAccessor();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IScopedErrorService, ScopedErrorService>();
        services.AddScoped<IScopedLogService, ScopedLogService>();
        services.AddScoped(typeof(IExceptionService),
            isDevelopment ? typeof(ExceptionInDevelopmentService) : typeof(ExceptionInProductionService));
        services.AddHttpClient<ISlackClient, SlackClient>(client =>
        {
            client.BaseAddress = new Uri("https://hooks.slack.com");
        });

        return services;
    }
}
