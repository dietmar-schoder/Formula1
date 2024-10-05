using Formula1.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1.Infrastructure.Persistence.Configurations;

public class CircuitConfiguration
{
    public static void Configure(EntityTypeBuilder<Circuit> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever(); // circuit_key https://api.openf1.org/v1/meetings?year=2024
        builder.Property(e => e.Name).IsRequired().HasMaxLength(1023);
    }
}
