using System.Text.Json;
using CodeDialect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDialect.Infrastructure.Persistence.Configurations;

public class ComparisonConfiguration : IEntityTypeConfiguration<Comparison>
{
    public void Configure(EntityTypeBuilder<Comparison> builder)
    {
        builder.Property(c => c.ComparisonNotes)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new()
            );
    }
}

public class ComparisonImplementationConfiguration : IEntityTypeConfiguration<ComparisonImplementation>
{
    public void Configure(EntityTypeBuilder<ComparisonImplementation> builder)
    {
        builder.HasKey(ci => new { ci.ComparisonId, ci.ImplementationId });

        builder.HasOne(ci => ci.Comparison)
            .WithMany(c => c.Implementations)
            .HasForeignKey(ci => ci.ComparisonId);

        builder.HasOne(ci => ci.Implementation)
            .WithMany()
            .HasForeignKey(ci => ci.ImplementationId);
    }
}
