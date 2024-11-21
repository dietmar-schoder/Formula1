using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetDrivers(IApplicationDbContext dbContext)
    : IRequestHandler<GetDrivers.Query, DriversPaginatedDto<DriverDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int PageNumber, int PageSize) : IRequest<DriversPaginatedDto<DriverDto>> { }

    public async Task<DriversPaginatedDto<DriverDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Drivers.CountAsync(cancellationToken);
        var driverDtos = await _dbContext.FORMULA1_Drivers
            .AsNoTracking()
            .OrderBy(d => d.Name)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(d => DriverDto.FromDriver(d))
            .ToListAsync(cancellationToken);
        return new DriversPaginatedDto<DriverDto>(
            driverDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
