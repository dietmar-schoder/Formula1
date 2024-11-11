using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonConstructorResults(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService),
        IRequestHandler<GetSeasonConstructorResults.Query, ResultsPaginatedDto<ConstructorResultDto>>
{
    public record Query(int Year, int ConstructorId) : IRequest<ResultsPaginatedDto<ConstructorResultDto>> { }

    public async Task<ResultsPaginatedDto<ConstructorResultDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var resultDtos = new List<ConstructorResultDto>();
        var results = await _dbContext.FORMULA1_Results
            .Where(r => r.ConstructorId == query.ConstructorId &&
                        r.Session.Race.SeasonYear == query.Year)
            .Include(r => r.Constructor)
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
            .ToListAsync(cancellationToken);
        foreach (var result in results)
        {
            var resultDto = result.Adapt<ConstructorResultDto>();
            resultDto.ConstructorName = result.Constructor.Name;
            resultDto.DriverName = result.Driver.Name;
            resultDto.SessionTypeId = result.Session.SessionTypeId;
            resultDto.SessionTypeDescription = result.Session.SessionType.Description;
            resultDto.RaceId = result.Session.RaceId;
            resultDto.SeasonYear = result.Session.Race.SeasonYear;
            resultDto.Round = result.Session.Race.Round;
            resultDto.GrandPrixId = result.Session.Race.GrandPrixId;
            resultDto.GrandPrixName = result.Session.Race.GrandPrix.Name;
            resultDto.CircuitId = result.Session.Race.CircuitId ?? 0;
            resultDto.CircuitName = result.Session.Race.Circuit.Name;
            resultDtos.Add(resultDto);
        }
        return new ResultsPaginatedDto<ConstructorResultDto>(
            resultDtos,
            1,
            results.Count,
            results.Count);
    }
}
