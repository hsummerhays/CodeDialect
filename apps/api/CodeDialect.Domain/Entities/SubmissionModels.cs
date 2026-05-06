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

public class Score : BaseEntity
{
    public Guid SubmissionId { get; set; }
    public Submission? Submission { get; set; }

    public int OverallScore { get; set; } // 0-100
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
    
    // Links to multiple implementations for side-by-side view
    public List<Guid> ImplementationIds { get; set; } = new();
    public List<string> ComparisonNotes { get; set; } = new();
}
