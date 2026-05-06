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

        // Value converter so Dictionary<string,string> works with all providers (including InMemory)
        builder.Entity<ExecutionProfile>()
            .Property(e => e.EnvironmentVariables)
            .HasConversion(
                v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new()
            );

        if (Database.ProviderName == "Npgsql.EntityFrameworkCore.PostgreSQL")
        {
            builder.Entity<Challenge>().Property(c => c.Tags).HasColumnType("text[]");
            builder.Entity<Dialect>().Property(d => d.SyntaxFeatures).HasColumnType("text[]");
            builder.Entity<ExecutionProfile>().Property(e => e.EnvironmentVariables).HasColumnType("jsonb");
        }
    }
}

public class ApplicationRole : Microsoft.AspNetCore.Identity.IdentityRole<Guid> { }
