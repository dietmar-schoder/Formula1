using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Formula1.Contracts.Dtos.PaginatedDtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasons(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetSeasons.Query, SeasonsPaginatedDto>
{
    public record Query(int PageNumber, int PageSize) : IRequest<SeasonsPaginatedDto> { }

    public async Task<SeasonsPaginatedDto> Handle(Query query, CancellationToken cancellationToken)
    {
        Log();
        var totalCount = await _dbContext.FORMULA1_Seasons.CountAsync(cancellationToken);
        var seasons = await _dbContext.FORMULA1_Seasons
            .AsNoTracking()
            .OrderByDescending(e => e.Year)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        Log(seasons.Count.ToString(), nameof(seasons.Count));
        return new SeasonsPaginatedDto(
            seasons.Adapt<List<SeasonDto>>(),
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}
