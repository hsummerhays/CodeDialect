using CodeDialect.Domain.Common;
using CodeDialect.Domain.Enums;

namespace CodeDialect.Domain.Entities;

public class Challenge : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Difficulty Difficulty { get; set; }
    public Guid CategoryId { get; set; }
    public ChallengeCategory? Category { get; set; }
    public ICollection<ChallengeVersion> Versions { get; set; } = new List<ChallengeVersion>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    public ICollection<string> Tags { get; set; } = new List<string>();
}

public class ChallengeCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
}

public class ChallengeLanguage : BaseEntity
{
    public string Name { get; set; } = string.Empty; // e.g. C#, Java, JavaScript
    public string Slug { get; set; } = string.Empty;
    public ICollection<ChallengeVersion> Versions { get; set; } = new List<ChallengeVersion>();
}

public class ChallengeVersion : BaseEntity
{
    public string Name { get; set; } = string.Empty; // e.g. .NET 10, Java 21, ES6
    public Guid LanguageId { get; set; }
    public ChallengeLanguage? Language { get; set; }
    public Guid ChallengeId { get; set; }
    public Challenge? Challenge { get; set; }
    public string BoilerplateCode { get; set; } = string.Empty;
    public string SolutionTemplate { get; set; } = string.Empty;
}

public class Submission : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ChallengeId { get; set; }
    public Guid VersionId { get; set; }
    public string Code { get; set; } = string.Empty;
    public SubmissionStatus Status { get; set; }
    public SubmissionResult? Result { get; set; }
    public Challenge? Challenge { get; set; }
    public ChallengeVersion? Version { get; set; }
}

public class SubmissionResult : BaseEntity
{
    public Guid SubmissionId { get; set; }
    public Submission? Submission { get; set; }
    public int Score { get; set; }
    public string Output { get; set; } = string.Empty;
    public double ExecutionTimeMs { get; set; }
    public long MemoryUsedBytes { get; set; }
}

public class SyntaxComparison : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid LeftVersionId { get; set; }
    public ChallengeVersion? LeftVersion { get; set; }
    public string LeftCode { get; set; } = string.Empty;
    public Guid RightVersionId { get; set; }
    public ChallengeVersion? RightVersion { get; set; }
    public string RightCode { get; set; } = string.Empty;
}
