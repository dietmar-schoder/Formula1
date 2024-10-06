using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetSeasonsQuery, List<SeasonDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<List<SeasonDto>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_Seasons
            .AsNoTracking()
            .OrderBy(e => e.Year)
            .ToListAsync(cancellationToken))
            .Adapt<List<SeasonDto>>();
}
