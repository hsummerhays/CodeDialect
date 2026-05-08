using CodeDialect.Domain.Common;
using CodeDialect.Domain.Enums;

namespace CodeDialect.Domain.Entities;

public class Submission : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ChallengeId { get; set; }
    public Challenge? Challenge { get; set; }
    public Guid DialectId { get; set; }
    public Dialect? Dialect { get; set; }

    public string SubmittedCode { get; set; } = string.Empty;
    public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;

    public Score? Score { get; set; }
}

// Owned value object — no identity, no separate table key.
// Configured via OwnsOne in SubmissionConfiguration.
public class Score
{
    public int OverallScore { get; set; }
    public int SyntaxAccuracy { get; set; }
    public int PerformanceScore { get; set; }
    public double ExecutionTimeMs { get; set; }
    public long MemoryUsedBytes { get; set; }
    public string? RunnerLogs { get; set; }
    public string? ErrorMessage { get; set; }
}

public class Comparison : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> ComparisonNotes { get; set; } = new();
    public ICollection<ComparisonImplementation> Implementations { get; set; } = new List<ComparisonImplementation>();
}

// Junction table with an explicit Order field — requires a modeled relationship
// rather than an implicit many-to-many.
public class ComparisonImplementation
{
    public Guid ComparisonId { get; set; }
    public Comparison? Comparison { get; set; }
    public Guid ImplementationId { get; set; }
    public ChallengeImplementation? Implementation { get; set; }
    public int Order { get; set; }
}
