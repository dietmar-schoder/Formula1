using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetGrandPrixQueryHandler(
    IApplicationDbContext dbContext,
    IScopedLogService logService,
    IScopedErrorService errorService)
    : HandlerBase(dbContext, logService, errorService), IRequestHandler<GetGrandPrixQuery, List<GrandPrixDto>>
{
    public async Task<List<GrandPrixDto>> Handle(GetGrandPrixQuery request, CancellationToken cancellationToken)
    {
        Log();
        var grandPrix = await _dbContext.FORMULA1_GrandPrix
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
        Log(grandPrix.Count.ToString(), nameof(grandPrix.Count));
        return grandPrix.Adapt<List<GrandPrixDto>>();
    }
}
