using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class SessionTypesEndpoints
{
    public static void MapSessionTypesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sessiontypes", ListSessionTypesAsync);

        static async Task<IResult> ListSessionTypesAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetSessionTypesQuery()));
    }
}
