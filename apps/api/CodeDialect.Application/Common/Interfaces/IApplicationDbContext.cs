using CodeDialect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeDialect.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Challenge> Challenges { get; }
    DbSet<ChallengeCategory> Categories { get; }
    DbSet<ChallengeLanguage> Languages { get; }
    DbSet<ChallengeVersion> Versions { get; }
    DbSet<Submission> Submissions { get; }
    DbSet<SubmissionResult> Results { get; }
    DbSet<SyntaxComparison> Comparisons { get; }
    DbSet<Score> Scores { get; }
    DbSet<Badge> Badges { get; }
    DbSet<LearningPath> LearningPaths { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
