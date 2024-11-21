using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetDriverConstructors(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetDriverConstructors.Query, List<ConstructorDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<List<ConstructorDto>> { }

    public async Task<List<ConstructorDto>> Handle(Query query, CancellationToken cancellationToken)
    {
        var constructorDtos = await _dbContext.FORMULA1_Results
            .AsNoTracking()
            .Where(r => r.DriverId == query.Id)
            .GroupBy(r => r.Constructor)
            .Select(g => ConstructorDto.FromConstructor(g.Key))
            .ToListAsync(cancellationToken);
        return [.. constructorDtos.OrderBy(c => c.Name)];
    }
}
