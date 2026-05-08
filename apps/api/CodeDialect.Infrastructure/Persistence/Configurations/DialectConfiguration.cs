using System.Text.Json;
using CodeDialect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDialect.Infrastructure.Persistence.Configurations;

public class DialectConfiguration : IEntityTypeConfiguration<Dialect>
{
    public void Configure(EntityTypeBuilder<Dialect> builder)
    {
        builder.Property(d => d.SyntaxFeatures)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new()
            );
    }
}
