using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers.Constructors;

public class GetConstructor(IApplicationDbContext dbContext)
    : IRequestHandler<GetConstructor.Query, ConstructorDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<ConstructorDto> { }

    public async Task<ConstructorDto> Handle(Query query, CancellationToken cancellationToken)
    {
        var constructor = await _dbContext.FORMULA1_Constructors
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);
        return constructor is null ? null : ConstructorDto.FromConstructor(constructor);
    }
}
