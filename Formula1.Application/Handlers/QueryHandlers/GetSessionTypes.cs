using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSessionTypes(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetSessionTypes.Query, List<SessionTypeDto>>
{
    public record Query() : IRequest<List<SessionTypeDto>> { }

    public async Task<List<SessionTypeDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        Log();
        var sessionTypes = await _dbContext.FORMULA1_SessionTypes
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .ToListAsync(cancellationToken);
        Log(sessionTypes.Count.ToString(), nameof(sessionTypes.Count));
        return sessionTypes.Adapt<List<SessionTypeDto>>();
    }
}
