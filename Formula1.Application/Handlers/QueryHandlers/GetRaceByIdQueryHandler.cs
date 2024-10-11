using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetRaceByIdQueryHandler(
    IApplicationDbContext context,
    IScopedLogService logService)
    : HandlerBase(context, logService), IRequestHandler<GetRaceByIdQuery, RaceDto>
{
    public async Task<RaceDto> Handle(GetRaceByIdQuery request, CancellationToken cancellationToken)
    {
        _logService.Log(request.Id.ToString(), nameof(request.Id));
        var race = await _context.FORMULA1_Races
            .AsNoTracking()
            .Include(e => e.Circuit)
            .Include(e => e.Season)
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new Exception("404");
        _logService.Log(race.Id.ToString(), nameof(race.Id));
        return race.Adapt<RaceDto>();
    }
}
