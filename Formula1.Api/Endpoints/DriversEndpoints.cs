using Formula1.Api.Extensions;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class DriversEndpoints
{
    public static void MapDriversEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/drivers", ListDriversAsync);
        app.MapGet("/api/drivers/{id:guid}", GetDriverAsync);

        static async Task<IResult> ListDriversAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetDriversQuery()));

        static async Task<IResult> GetDriverAsync(Guid id, IMediator mediator, IScopedErrorService errorService)
            => await mediator.SendQueryAsync(new GetDriverByIdQuery(id), errorService);
    }
}
