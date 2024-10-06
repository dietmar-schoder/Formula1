﻿using Formula1.Contracts.Services;
using Formula1.Domain.Common.Interfaces;
using Formula1.Infrastructure.ExternalApis;
using Formula1.Infrastructure.Services;

namespace Formula1.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddHttpClient<IErgastApisClient, ErgastApisClient>(client =>
        {
            client.BaseAddress = new Uri("https://ergast.com"); // https://ergast.com
        });

        return services;
    }
}
