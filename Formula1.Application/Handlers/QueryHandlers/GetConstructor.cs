using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructor(
    IApplicationDbContext dbContext)
    : IRequestHandler<GetConstructor.Query, ConstructorDto>
{
    protected readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<ConstructorDto> { }

    public async Task<ConstructorDto> Handle(Query query, CancellationToken cancellationToken)
        => (await _dbContext.FORMULA1_Constructors
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == query.Id, cancellationToken))?
            .Adapt<ConstructorDto>();
}
