using Formula1.Application.Handlers.QueryHandlers.Sessions;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class SessionTypesEndpoints
{
    public static void MapSessionTypesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sessiontypes", ListSessionTypesAsync);

        static async Task<IResult> ListSessionTypesAsync(IMediator mediator, int pageNumber = 1, int PageSize = 20)
            => Results.Ok(await mediator.Send(new GetSessionTypes.Query(pageNumber, PageSize)));
    }
}
