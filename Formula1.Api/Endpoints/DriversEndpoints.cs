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
        //app.MapGet("/api/drivers/{id:int}", GetDriverAsync);

        //app.MapGet("/api/drivers/{id:int}/results", GetDriverAsync);
        //app.MapGet("/api/drivers/{id:int}/constructors", GetDriverAsync);
        //app.MapGet("/api/drivers/{id:int}/races", GetDriverAsync);
        //app.MapGet("/api/drivers/{id:int}/seasons", GetDriverAsync);

        static async Task<IResult> ListDriversAsync(IMediator mediator, int pageNumber = 1, int pageSize = 20)
            => Results.Ok(await mediator.Send(new GetDrivers.Query(pageNumber, pageSize)));

        //static async Task<IResult> GetDriverAsync(
        //    int id,
        //    IMediator mediator,
        //    IScopedErrorService errorService)
        //    => await mediator.SendQueryAsync(new GetDriverByIdQuery(id), errorService);

        //static async Task<IResult> GetDriverResultsAsync(
        //    IMediator mediator,
        //    IScopedErrorService errorService,
        //    Guid id,
        //    int pageNumber = 1,
        //    int pageSize = 20)
        //    => await mediator.SendQueryAsync(new GetDriverResults.Query(id, pageNumber, pageSize), errorService);
    }
}
