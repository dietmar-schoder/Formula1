using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetDrivers(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService),
        IRequestHandler<GetDrivers.Query, DriversPaginatedDto<DriverDto>>
{
    public record Query(int PageNumber, int PageSize) : IRequest<DriversPaginatedDto<DriverDto>> { }

    public async Task<DriversPaginatedDto<DriverDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        Log();
        var totalCount = await _dbContext.FORMULA1_Drivers.CountAsync(cancellationToken);
        var drivers = await _dbContext.FORMULA1_Drivers
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        Log(drivers.Count.ToString(), nameof(drivers.Count));
        return new DriversPaginatedDto<DriverDto>(
            drivers.Adapt<List<DriverDto>>(),
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
