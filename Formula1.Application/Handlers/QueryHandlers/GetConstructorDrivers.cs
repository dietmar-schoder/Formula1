using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructorDrivers(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetConstructorDrivers.Query, List<DriverDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<List<DriverDto>> { }

    public async Task<List<DriverDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var driverDtos = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(r => r.ConstructorId == query.Id)
            .GroupBy(r => r.Driver)
            .Select(g => DriverDto.FromDriver(g.Key))
            .ToListAsync(cancellationToken);
        return [.. driverDtos.OrderBy(c => c.Name)];
    }
}
