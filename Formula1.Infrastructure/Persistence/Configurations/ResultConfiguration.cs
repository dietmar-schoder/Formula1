using Formula1.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1.Infrastructure.Persistence.Configurations;

public class ResultConfiguration
{
    public static void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
