using Formula1.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1.Infrastructure.Persistence.Configurations;

public class CircuitConfiguration
{
    public static void Configure(EntityTypeBuilder<Circuit> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(1023);
        builder.Property(e => e.ErgastCircuitId).HasMaxLength(255);
    }
}
