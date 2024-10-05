using Formula1.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1.Infrastructure.Persistence.Configurations;

public class RaceConfiguration
{
    public static void Configure(EntityTypeBuilder<Race> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever(); // meeting_key https://api.openf1.org/v1/meetings?year=2024
    }
}
