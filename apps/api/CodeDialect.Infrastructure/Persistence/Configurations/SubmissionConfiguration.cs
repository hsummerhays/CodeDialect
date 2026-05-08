using CodeDialect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDialect.Infrastructure.Persistence.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        // Score is an optional owned value object embedded in the Submission table.
        builder.OwnsOne(s => s.Score);
    }
}
