using Formula1.Application.Interfaces.Services;
using Formula1.Domain.Common.Interfaces;
using Formula1.Infrastructure.Services;

namespace Formula1.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IScopedLogService, ScopedLogService>();

        return services;
    }
}
