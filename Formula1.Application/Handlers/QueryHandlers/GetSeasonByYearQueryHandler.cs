using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonByYearQueryHandler(
    IApplicationDbContext context,
    IScopedLogService logService)
    : HandlerBase(context, logService), IRequestHandler<GetSeasonByYearQuery, SeasonDto>
{
    public async Task<SeasonDto> Handle(GetSeasonByYearQuery request, CancellationToken cancellationToken)
    {
        Log(request.Year.ToString(), nameof(request.Year));
        var season = await _context.FORMULA1_Seasons
            .AsNoTracking()
            .Include(s => s.Races.OrderBy(r => r.Round))
                .ThenInclude(r => r.Circuit)
            .SingleOrDefaultAsync(s => s.Year == request.Year, cancellationToken)
            ?? ThrowNotFoundError<Season>(request.Year.ToString());
        Log(season.Year.ToString(), nameof(season.Year));
        return season.Adapt<SeasonDto>();
    }
}
