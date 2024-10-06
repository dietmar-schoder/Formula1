using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetRaceByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetRaceByIdQuery, RaceDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<RaceDto> Handle(GetRaceByIdQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_Races
            .AsNoTracking()
            .Include(e => e.Circuit)
            .Include(e => e.Season)
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken))?
            .Adapt<RaceDto>();
}
