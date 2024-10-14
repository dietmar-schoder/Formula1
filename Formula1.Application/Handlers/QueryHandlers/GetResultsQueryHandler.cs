using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetResultsQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetResultsQuery, List<ResultDto>>
{
    public async Task<List<ResultDto>> Handle(GetResultsQuery request, CancellationToken cancellationToken)
    {
        Log();
        var results = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Include(e => e.Session)
            .Include(e => e.Driver)
            .Include(e => e.Constructor)
            .OrderByDescending(e => e.Session.StartDateTimeUtc)
            .ThenBy(e => e.Session.SessionTypeId)
            .ToListAsync(cancellationToken);
        Log(results.Count.ToString(), nameof(results.Count));
        return results.Adapt<List<ResultDto>>();
    }
}
