using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Interfaces.Services;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Formula1.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructorByIdQueryHandler(
    IApplicationDbContext context,
    IScopedLogService logService)
    : HandlerBase(context, logService), IRequestHandler<GetConstructorByIdQuery, ConstructorDto>
{
    public async Task<ConstructorDto> Handle(GetConstructorByIdQuery request, CancellationToken cancellationToken)
    {
        _logService.Log(request.Id.ToString(), nameof(request.Id));
        var constructor = await _context.FORMULA1_Constructors
            .AsNoTracking()
            .Include(e => e.Results.OrderBy(r => r.Session.StartDateTimeUtc))
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new Exception("404");
        _logService.Log(constructor.Id.ToString(), nameof(constructor.Id));
        return constructor.Adapt<ConstructorDto>();
    }
}
