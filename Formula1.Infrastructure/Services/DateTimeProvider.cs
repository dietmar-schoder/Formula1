using Formula1.Domain.Common.Interfaces;

namespace Formula1.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
