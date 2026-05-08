using CodeDialect.Domain.Common;
using CodeDialect.Domain.Enums;

namespace CodeDialect.Domain.Entities;

public class Challenge : BaseEntity
{
    private string _title = string.Empty;

    public string Title
    {
        get => _title;
        set => _title = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Title cannot be empty.", nameof(value))
            : value;
    }

    public string Description { get; set; } = string.Empty;
    public Difficulty Difficulty { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<string> Tags { get; set; } = new();

    public ICollection<ChallengeImplementation> Implementations { get; set; } = new List<ChallengeImplementation>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}

public class ChallengeImplementation : BaseEntity
{
    public Guid ChallengeId { get; set; }
    public Challenge? Challenge { get; set; }
    public Guid DialectId { get; set; }
    public Dialect? Dialect { get; set; }

    public string StarterCode { get; set; } = string.Empty;
    public string ReferenceSolution { get; set; } = string.Empty;
    public string TestCases { get; set; } = string.Empty; // Store as JSON or script
    
    public Guid ExecutionProfileId { get; set; }
    public ExecutionProfile? ExecutionProfile { get; set; }
}

public class ExecutionProfile : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int TimeoutMs { get; set; } = 5000;
    public long MemoryLimitBytes { get; set; } = 128 * 1024 * 1024; // 128MB
    public string DockerImage { get; set; } = string.Empty;
    public Dictionary<string, string> EnvironmentVariables { get; set; } = new();
}
