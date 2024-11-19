using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasons(IApplicationDbContext dbContext)
    : IRequestHandler<GetSeasons.Query, SeasonsPaginatedDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int PageNumber, int PageSize) : IRequest<SeasonsPaginatedDto> { }

    public async Task<SeasonsPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext.FORMULA1_Seasons.CountAsync(cancellationToken);
        var seasonDtos = await _dbContext.FORMULA1_Seasons
            .AsNoTracking()
            .OrderByDescending(s => s.Year)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(s => SeasonDto.FromSeason(s))
            .ToListAsync(cancellationToken);
        return new SeasonsPaginatedDto(
            seasonDtos,
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
