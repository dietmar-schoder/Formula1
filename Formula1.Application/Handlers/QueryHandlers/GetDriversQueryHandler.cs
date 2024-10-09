using Formula1.Application.Interfaces.Persistence;
using Formula1.Application.Queries;
using Formula1.Contracts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Handlers.QueryHandlers
{
    public class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, List<DriverBasicDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetDriversQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DriverBasicDto>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            var drivers = await _context.FORMULA1_Drivers
                .Select(d => new DriverBasicDto
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .OrderBy(d => d.Name)
                .ToListAsync(cancellationToken);

            return drivers;
        }
    }
}
