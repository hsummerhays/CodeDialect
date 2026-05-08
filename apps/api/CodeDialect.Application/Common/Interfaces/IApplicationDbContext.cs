using CodeDialect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeDialect.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Challenge> Challenges { get; }
    DbSet<Category> Categories { get; }
    DbSet<Language> Languages { get; }
    DbSet<Dialect> Dialects { get; }
    DbSet<ChallengeImplementation> Implementations { get; }
    DbSet<ExecutionProfile> ExecutionProfiles { get; }
    DbSet<Submission> Submissions { get; }
    // Score is owned by Submission — accessed via Submission.Score, no DbSet needed
    DbSet<Comparison> Comparisons { get; }
    DbSet<ComparisonImplementation> ComparisonImplementations { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
