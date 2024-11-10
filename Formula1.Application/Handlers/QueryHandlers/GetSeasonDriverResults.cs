using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonDriverResults(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService),
        IRequestHandler<GetSeasonDriverResults.Query, ResultsPaginatedDto<ResultDto>>
{
    public record Query(int Year, Guid DriverId) : IRequest<ResultsPaginatedDto<ResultDto>> { }

    public async Task<ResultsPaginatedDto<ResultDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var results = await _dbContext.FORMULA1_Results
            .Where(r => r.DriverId == query.DriverId &&
                        r.Session.Race.SeasonYear == query.Year)
            .Include(r => r.Driver)
            .Include(r => r.Constructor)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.GrandPrix)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .OrderBy(r => r.Session.Race.Round)
            .ToListAsync(cancellationToken);
        return new ResultsPaginatedDto<ResultDto>(
            results.Adapt<List<ResultDto>>(),
            1,
            results.Count,
            results.Count);
    }
}
