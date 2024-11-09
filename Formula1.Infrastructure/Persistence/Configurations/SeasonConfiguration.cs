using Formula1.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1.Infrastructure.Persistence.Configurations;

public class SeasonConfiguration
{
    public static void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.HasKey(e => e.Year);
        builder.Property(e => e.Year).ValueGeneratedNever();
        builder.Property(e => e.WikipediaUrl).HasMaxLength(1023);
    }
}
