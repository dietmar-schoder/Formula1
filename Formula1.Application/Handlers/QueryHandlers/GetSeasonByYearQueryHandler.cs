using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonByYearQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetSeasonByYearQuery, SeasonDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<SeasonDto> Handle(GetSeasonByYearQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_Seasons
            .AsNoTracking()
            .Include(s => s.Races.OrderBy(r => r.Round))
                .ThenInclude(r => r.Circuit)
            .SingleOrDefaultAsync(s => s.Year == request.Year, cancellationToken))?
            .Adapt<SeasonDto>();
}
