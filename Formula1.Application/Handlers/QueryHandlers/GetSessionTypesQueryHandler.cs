using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionTypesQueryHandler(
    IApplicationDbContext context,
    IScopedLogService logService)
    : HandlerBase(context, logService), IRequestHandler<GetSessionTypesQuery, List<SessionTypeDto>>
{
    public async Task<List<SessionTypeDto>> Handle(GetSessionTypesQuery request, CancellationToken cancellationToken)
    {
        _logService.Log();
        var sessionTypes = await _context.FORMULA1_SessionTypes
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .ToListAsync(cancellationToken);
        _logService.Log(sessionTypes.Count.ToString(), nameof(sessionTypes.Count));
        return sessionTypes.Adapt<List<SessionTypeDto>>();
    }
}
