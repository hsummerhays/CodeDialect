using System.Text.Json;
using CodeDialect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDialect.Infrastructure.Persistence.Configurations;

public class ExecutionProfileConfiguration : IEntityTypeConfiguration<ExecutionProfile>
{
    public void Configure(EntityTypeBuilder<ExecutionProfile> builder)
    {
        builder.Property(e => e.EnvironmentVariables)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null) ?? new()
            );
    }
}
