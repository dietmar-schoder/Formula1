using Formula1.Application.Handlers.QueryHandlers;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class DriversEndpoints
{
    public static void MapDriversEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/drivers", ListDriversAsync);
        app.MapGet("/api/drivers/{id:int}", GetDriverAsync);
        app.MapGet("/api/drivers/{id:int}/results", GetDriverResultsAsync);
        app.MapGet("/api/drivers/{id:int}/constructors", GetDriverConstructorsAsync);
        //app.MapGet("/api/drivers/{id:int}/races", GetDriverAsync);
        //app.MapGet("/api/drivers/{id:int}/seasons", GetDriverAsync);

        static async Task<IResult> ListDriversAsync(IMediator mediator, int pageNumber = 1, int pageSize = 20)
            => Results.Ok(await mediator.Send(new GetDrivers.Query(pageNumber, pageSize)));

        static async Task<IResult> GetDriverAsync(int id, IMediator mediator)
            => await mediator.SendQueryAsync(new GetDriver.Query(id));

        static async Task<IResult> GetDriverResultsAsync(IMediator mediator, int id, int pageNumber = 1, int pageSize = 20)
            => await mediator.SendQueryAsync(new GetDriverResults.Query(id, pageNumber, pageSize));

        static async Task<IResult> GetDriverConstructorsAsync(IMediator mediator, int id)
            => await mediator.SendQueryAsync(new GetDriverConstructors.Query(id));
    }
}
