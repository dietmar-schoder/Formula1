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
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetResults.Query, ResultsPaginatedDto<ResultDto>>
{
    public record Query(int PageNumber, int PageSize) : IRequest<ResultsPaginatedDto<ResultDto>> { }

    public async Task<ResultsPaginatedDto<ResultDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var resultDtos = new List<ResultDto>();
        var totalCount = await _dbContext.FORMULA1_Results.CountAsync(cancellationToken);
        var results = await _dbContext.FORMULA1_Results
            .AsNoTracking()

            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(s => s.GrandPrix)

            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.Circuit)

            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)

            .Include(r => r.Driver)

            .Include(r => r.Constructor)

            .OrderByDescending(r => r.Session.Race.SeasonYear)
                .ThenBy(r => r.Session.Race.Round)
                .ThenBy(r => r.Session.SessionTypeId)
                .ThenBy(r => r.Position)

            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)

            .ToListAsync(cancellationToken);
        foreach (var result in results)
        {
            var resultDto = result.Adapt<ResultDto>();
            resultDto.Points = result.Points / 100m;
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
        return new ResultsPaginatedDto<ResultDto>(
            resultDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
