using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers
{
    public class GetDriversQueryHandler(
        IApplicationDbContext context,
        IScopedLogService logService)
        : HandlerBase(context, logService), IRequestHandler<GetDriversQuery, List<DriverBasicDto>>
    {
        public async Task<List<DriverBasicDto>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            _logService.Log();
            var drivers = await _context.FORMULA1_Drivers
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .ToListAsync(cancellationToken);
            _logService.Log(drivers.Count.ToString(), nameof(drivers.Count));
            return drivers.Adapt<List<DriverBasicDto>>();
        }
    }
}
