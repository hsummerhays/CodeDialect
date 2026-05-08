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
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<Dialect> Dialects => Set<Dialect>();
    public DbSet<ChallengeImplementation> Implementations => Set<ChallengeImplementation>();
    public DbSet<ExecutionProfile> ExecutionProfiles => Set<ExecutionProfile>();
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<Comparison> Comparisons => Set<Comparison>();
    public DbSet<ComparisonImplementation> ComparisonImplementations => Set<ComparisonImplementation>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

public class ApplicationRole : Microsoft.AspNetCore.Identity.IdentityRole<Guid> { }
