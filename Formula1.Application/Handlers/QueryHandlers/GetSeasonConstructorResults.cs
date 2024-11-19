using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonConstructorResults(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService),
        IRequestHandler<GetSeasonConstructorResults.Query, List<SeasonConstructorResultDto>>
{
    public record Query(int Year, int ConstructorId) : IRequest<List<SeasonConstructorResultDto>> { }

    public async Task<List<SeasonConstructorResultDto>> Handle(Query query, CancellationToken cancellationToken)
        => await _dbContext.FORMULA1_Results
            .Where(r => r.ConstructorId == query.ConstructorId &&
                        r.Session.Race.SeasonYear == query.Year)
            .Include(r => r.Driver)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.GrandPrix)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.Circuit)
            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)
            .OrderBy(r => r.Session.Race.Round)
                .ThenBy(r => r.Position)
            .Select(r => SeasonConstructorResultDto.FromResult(r))
            .ToListAsync(cancellationToken);
}
