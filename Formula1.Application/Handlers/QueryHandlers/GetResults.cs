using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetResults(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetResults.Query, ResultsPaginatedDto>
{
    public record Query(int PageNumber, int PageSize) : IRequest<ResultsPaginatedDto> { }

    public async Task<ResultsPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        Log();
        var totalCount = await _dbContext.FORMULA1_Results.CountAsync(cancellationToken);
        var results = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Include(r => r.Session)
            .Include(r => r.Driver)
            .Include(r => r.Constructor)
            .OrderByDescending(r => r.Session.Race.SeasonYear)
                .ThenBy(r => r.Session.Race.Round)
                .ThenBy(r => r.Session.SessionTypeId)
                .ThenBy(r => r.Position)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        Log(results.Count.ToString(), nameof(results.Count));
        return new ResultsPaginatedDto(
            results.Adapt<List<ResultDto>>(),
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
