using Formula1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<SessionType> FORMULA1_SessionTypes { get; set; }

    DbSet<Session> FORMULA1_Sessions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
