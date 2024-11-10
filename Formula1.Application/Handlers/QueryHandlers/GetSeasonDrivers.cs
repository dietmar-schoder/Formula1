using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonDrivers(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService),
        IRequestHandler<GetSeasonDrivers.Query, DriversPaginatedDto<DriverPointsDto>>
{
    public record Query(int Year) : IRequest<DriversPaginatedDto<DriverPointsDto>> { }

    public async Task<DriversPaginatedDto<DriverPointsDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var drivers = new List<DriverPointsDto>();
        var driversPoints = await _dbContext.FORMULA1_Results
            .Where(r => r.Session.Race.SeasonYear == query.Year)
            .GroupBy(r => r.Driver)
            .Select(g => new
            {
                Driver = g.Key,
                Points = g.Sum(r => r.Points)
            })
            .OrderByDescending(g => g.Points)
            .ToListAsync(cancellationToken);
        foreach (var entry in driversPoints)
        {
            var driverPoints = entry.Driver.Adapt<DriverPointsDto>();
            driverPoints.Points = entry.Points;
            drivers.Add(driverPoints);
        }
        return new DriversPaginatedDto<DriverPointsDto>(
            drivers,
            1,
            drivers.Count,
            drivers.Count);
    }
}
