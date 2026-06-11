using CodeDialect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDialect.Infrastructure.Persistence.Configurations;

public class DialectConfiguration : IEntityTypeConfiguration<Dialect>
{
    public void Configure(EntityTypeBuilder<Dialect> builder)
    {
        builder.Property(d => d.SyntaxFeatures)
            .HasConversion(JsonValueConverters.StringList());
    }
}
