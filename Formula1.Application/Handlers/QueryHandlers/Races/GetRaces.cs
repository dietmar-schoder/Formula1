using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Races;

public class GetRaces(IApplicationDbContext dbContext)
    : IRequestHandler<GetRaces.Query, RacesPaginatedDto<RaceDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int PageNumber, int PageSize) : IRequest<RacesPaginatedDto<RaceDto>> { }

    public async Task<RacesPaginatedDto<RaceDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(query.PageSize, 100);
        var totalCount = await _dbContext.FORMULA1_Races.CountAsync(cancellationToken);
        var raceDtos = await _dbContext.FORMULA1_Races
            .AsNoTracking()
            .Include(r => r.Season)
            .Include(r => r.GrandPrix)
            .Include(r => r.Circuit)
            .OrderByDescending(e => e.SeasonYear)
                .ThenBy(r => r.Round)
            .Skip((query.PageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => RaceDto.FromRace(r))
            .ToListAsync(cancellationToken);
        return new RacesPaginatedDto<RaceDto>(
            raceDtos,
            query.PageNumber,
            pageSize,
            totalCount);
    }
}
