using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers;

public class GetConstructorByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetConstructorByIdQuery, ConstructorDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ConstructorDto> Handle(GetConstructorByIdQuery request, CancellationToken cancellationToken)
        => (await _context.FORMULA1_Constructors
            .AsNoTracking()
            .Include(e => e.Results.OrderBy(r => r.Session.StartDateTimeUtc))
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken))?
            .Adapt<ConstructorDto>();
}
