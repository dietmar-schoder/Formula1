using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeason(IApplicationDbContext dbContext)
    : IRequestHandler<GetSeason.Query, SeasonDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Year) : IRequest<SeasonDto> { }

    public async Task<SeasonDto> Handle(Query request, CancellationToken cancellationToken)
    {
        var season = await _dbContext.FORMULA1_Seasons
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Year == request.Year, cancellationToken);
        if (season is null) { return default; }
        return SeasonDto.FromSeason(season);
    }
}
