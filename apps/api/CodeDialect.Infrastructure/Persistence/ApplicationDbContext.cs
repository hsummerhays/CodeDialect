using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Domain.Entities;
using CodeDialect.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeDialect.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Challenge> Challenges => Set<Challenge>();
    public DbSet<ChallengeCategory> Categories => Set<ChallengeCategory>();
    public DbSet<ChallengeLanguage> Languages => Set<ChallengeLanguage>();
    public DbSet<ChallengeVersion> Versions => Set<ChallengeVersion>();
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<SubmissionResult> Results => Set<SubmissionResult>();
    public DbSet<SyntaxComparison> Comparisons => Set<SyntaxComparison>();
    public DbSet<Score> Scores => Set<Score>();
    public DbSet<Badge> Badges => Set<Badge>();
    public DbSet<LearningPath> LearningPaths => Set<LearningPath>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Feature-specific configurations
        builder.Entity<Challenge>().Property(c => c.Tags).HasColumnType("text[]");
    }
}

public class ApplicationRole : Microsoft.AspNetCore.Identity.IdentityRole<Guid> { }
