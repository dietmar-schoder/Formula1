using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonDrivers(IApplicationDbContext dbContext)
    : IRequestHandler<GetSeasonDrivers.Query, List<DriverDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Year) : IRequest<List<DriverDto>> { }

    public async Task<List<DriverDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var driverDtos = await _dbContext.FORMULA1_Results
                .Where(r => r.Session.Race.SeasonYear == query.Year)
                .GroupBy(r => r.Driver)
                .Select(g => DriverDto.FromDriver(g.Key))
                .ToListAsync(cancellationToken);
        return [.. driverDtos.OrderBy(d => d.Name)];
    }
}
