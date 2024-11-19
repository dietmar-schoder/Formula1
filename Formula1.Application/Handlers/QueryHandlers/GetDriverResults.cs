using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetDriverResults(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetDriverResults.Query, ResultsPaginatedDto<ResultDto>>
{
    protected readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id, int PageNumber, int PageSize) : IRequest<ResultsPaginatedDto<ResultDto>> { }

    public async Task<ResultsPaginatedDto<ResultDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var resultDtos = new List<ResultDto>();
        var totalCount = await _dbContext.FORMULA1_Results.Where(d => d.DriverId == query.Id).CountAsync(cancellationToken);
        var results = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(d => d.DriverId == query.Id)
            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(s => s.GrandPrix)

            .Include(r => r.Session)
                .ThenInclude(s => s.Race)
                    .ThenInclude(r => r.Circuit)

            .Include(r => r.Session)
                .ThenInclude(s => s.SessionType)

            .Include(r => r.Constructor)

            .OrderByDescending(r => r.Session.Race.SeasonYear)
                .ThenBy(r => r.Session.Race.Round)
                .ThenBy(r => r.Session.SessionTypeId)

            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)

            .ToListAsync(cancellationToken);
        foreach (var result in results)
        {
            var resultDto = result.Adapt<ResultDto>();
            resultDto.Points = result.Points / 100m;
            resultDto.ConstructorName = result.Constructor.Name;
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
