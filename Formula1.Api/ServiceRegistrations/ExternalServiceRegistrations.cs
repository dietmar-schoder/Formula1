using Formula1.Contracts.ExternalServices;
using Formula1.Infrastructure.ExternalApis;

namespace Formula1.Api.ServiceRegistrations;

public static class ExternalServiceRegistrations
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddHttpClient<ISlackClient, SlackClient>(client =>
        {
            client.BaseAddress = new Uri("https://hooks.slack.com");
        });

        return services;
    }
}
