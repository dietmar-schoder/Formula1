using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonsQueryHandler(
    IApplicationDbContext context,
    IScopedLogService logService)
    : HandlerBase(context, logService), IRequestHandler<GetSeasonsQuery, List<SeasonDto>>
{
    public async Task<List<SeasonDto>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
    {
        _logService.Log();
        var seasons = await _context.FORMULA1_Seasons
            .AsNoTracking()
            .OrderBy(e => e.Year)
            .ToListAsync(cancellationToken);
        _logService.Log(seasons.Count.ToString(), nameof(seasons.Count));
        return seasons.Adapt<List<SeasonDto>>();
    }
}
