using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Seasons;

public class GetSeasonConstructors(IApplicationDbContext dbContext)
    : IRequestHandler<GetSeasonConstructors.Query, List<ConstructorDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Year) : IRequest<List<ConstructorDto>> { }

    public async Task<List<ConstructorDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var constructorDtos = await _dbContext.FORMULA1_Results
                .Where(r => r.Session.Race.SeasonYear == query.Year)
                .GroupBy(r => r.Constructor)
                .Select(g => ConstructorDto.FromConstructor(g.Key))
                .ToListAsync(cancellationToken);
        return [.. constructorDtos.OrderBy(d => d.Name)];
    }
}
