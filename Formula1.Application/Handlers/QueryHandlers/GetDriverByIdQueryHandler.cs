using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetDriverByIdQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetDriverByIdQuery, DriverDto>
{
    public async Task<DriverDto> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
    {
        Log(request.Id.ToString(), nameof(request.Id));
        var driver = await _dbContext.FORMULA1_Drivers
            .AsNoTracking()
            .Include(e => e.Results.OrderByDescending(r => r.Session.StartDateTimeUtc))
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? await ReturnNotFoundErrorAsync<Driver>(request.Id.ToString());
        Log(driver.Id.ToString(), nameof(driver.Id));
        return driver.Adapt<DriverDto>();
    }
}
