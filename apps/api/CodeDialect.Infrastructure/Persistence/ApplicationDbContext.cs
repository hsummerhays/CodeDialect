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
    public DbSet<Score> Scores => Set<Score>();
    public DbSet<Comparison> Comparisons => Set<Comparison>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Feature-specific configurations for PostgreSQL arrays/JSON
        // We only apply these if we are using PostgreSQL
        if (Database.ProviderName == "Npgsql.EntityFrameworkCore.PostgreSQL")
        {
            builder.Entity<Challenge>().Property(c => c.Tags).HasColumnType("text[]");
            builder.Entity<Dialect>().Property(d => d.SyntaxFeatures).HasColumnType("text[]");
            
            // ExecutionProfile environment variables as JSON
            builder.Entity<ExecutionProfile>().Property(e => e.EnvironmentVariables)
                .HasColumnType("jsonb");
        }
    }
}

public class ApplicationRole : Microsoft.AspNetCore.Identity.IdentityRole<Guid> { }
