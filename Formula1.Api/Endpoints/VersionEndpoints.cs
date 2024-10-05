using Formula1.Application.Services;

namespace Formula1.Api.Endpoints;

public static class VersionEndpoints
{
    public static void MapVersionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", ServiceAlive);
        app.MapGet("/api", ServiceAlive)
            .WithTags("Version")
            .WithName("GetVersion");

        static IResult ServiceAlive(IVersionService versionService)
            => Results.Ok(versionService.GetVersion());
    }
}
