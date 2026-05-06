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
    DbSet<Score> Scores { get; }
    DbSet<Comparison> Comparisons { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
