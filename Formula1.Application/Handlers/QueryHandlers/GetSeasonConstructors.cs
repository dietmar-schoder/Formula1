using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetSeasonConstructors(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService),
        IRequestHandler<GetSeasonConstructors.Query, ConstructorsPaginatedDto<ConstructorPointsDto>>
{
    public record Query(int Year) : IRequest<ConstructorsPaginatedDto<ConstructorPointsDto>> { }

    public async Task<ConstructorsPaginatedDto<ConstructorPointsDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var constructors = new List<ConstructorPointsDto>();
        var constructorsPoints = await _dbContext.FORMULA1_Results
            .Where(r => r.Session.Race.SeasonYear == query.Year)
            .GroupBy(r => r.Constructor)
            .Select(g => new
            {
                Constructor = g.Key,
                Points = g.Sum(r => r.Points)
            })
            .OrderByDescending(g => g.Points)
            .ToListAsync(cancellationToken);
        foreach (var entry in constructorsPoints)
        {
            var constructorPoints = entry.Constructor.Adapt<ConstructorPointsDto>();
            constructorPoints.Points = entry.Points;
            constructors.Add(constructorPoints);
        }
        return new ConstructorsPaginatedDto<ConstructorPointsDto>(
            constructors,
            1,
            constructors.Count,
            constructors.Count);
    }
}
