using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructorDrivers(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetConstructorDrivers.Query, DriversPaginatedDto<DriverDto>>
{
    protected readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<DriversPaginatedDto<DriverDto>> { }

    public async Task<DriversPaginatedDto<DriverDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var drivers = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(r => r.ConstructorId == query.Id)
            .GroupBy(r => r.Driver)
            .Select(g => g.Key)
            .ToListAsync(cancellationToken);
        drivers = [.. drivers.OrderBy(c => c.Name)];
        return new DriversPaginatedDto<DriverDto>(
            drivers.Adapt<List<DriverDto>>(),
            1,
            drivers.Count,
            drivers.Count);
    }
}
