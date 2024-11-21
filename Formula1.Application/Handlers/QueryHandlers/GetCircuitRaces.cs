using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetCircuitRaces(IApplicationDbContext dbContext)
    : IRequestHandler<GetCircuitRaces.Query, RacesPaginatedDto<CircuitRaceDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id, int PageNumber, int PageSize) : IRequest<RacesPaginatedDto<CircuitRaceDto>> { }

    public async Task<RacesPaginatedDto<CircuitRaceDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext.FORMULA1_Races
            .Where(r => r.CircuitId == query.Id)
            .CountAsync(cancellationToken);
        var raceDtos = await _dbContext.FORMULA1_Races
            .Where(r => r.CircuitId == query.Id)
            .AsNoTracking()
            .Include(r => r.GrandPrix)
            .OrderByDescending(r => r.SeasonYear)
                .ThenBy(r => r.Round)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(r => CircuitRaceDto.FromRace(r))
            .ToListAsync(cancellationToken);
        return new RacesPaginatedDto<CircuitRaceDto>(
            raceDtos,
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
