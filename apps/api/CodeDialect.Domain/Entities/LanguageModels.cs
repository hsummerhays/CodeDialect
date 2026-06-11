using CodeDialect.Domain.Common;

namespace CodeDialect.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
}

public class Language : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public ICollection<Dialect> Dialects { get; set; } = new List<Dialect>();
}

public class Dialect : BaseEntity
{
    public Guid LanguageId { get; set; }
    public Language? Language { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Framework { get; set; }
    public string RuntimeVersion { get; set; } = string.Empty;
    public IReadOnlyList<string> SyntaxFeatures { get; set; } = new List<string>();

    public ICollection<ChallengeImplementation> Implementations { get; set; } = new List<ChallengeImplementation>();
}
