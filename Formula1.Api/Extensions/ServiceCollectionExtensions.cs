using Formula1.Application.Services;
using Formula1.Contracts.Services;
using Formula1.Domain.Common.Interfaces;
using Formula1.Infrastructure.Services;

namespace Formula1.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IDataImportService, DataImportService>();
        services.AddSingleton<IVersionService, VersionService>();
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddHttpClient<IOpenF1Service, OpenF1Service>(client =>
        {
            client.BaseAddress = new Uri("https://api.openf1.org/v1");
        });

        return services;
    }
}
