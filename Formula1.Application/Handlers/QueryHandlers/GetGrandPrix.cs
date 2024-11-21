using Formula1.Application.Interfaces.Persistence;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetGrandPrix(IApplicationDbContext dbContext)
    : IRequestHandler<GetGrandPrix.Query, GrandPrixDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public record Query(int Id) : IRequest<GrandPrixDto> { }

    public async Task<GrandPrixDto> Handle(Query request, CancellationToken cancellationToken)
    {
        var grandPrix = await _dbContext.FORMULA1_GrandPrix
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id.Equals(request.Id), cancellationToken);
        return grandPrix is null ? null : GrandPrixDto.FromGrandPrix(grandPrix);
    }
}
