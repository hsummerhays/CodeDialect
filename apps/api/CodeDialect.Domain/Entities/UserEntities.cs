using CodeDialect.Domain.Common;

namespace CodeDialect.Domain.Entities;

public class Score : BaseEntity
{
    public Guid UserId { get; set; }
    public int TotalPoints { get; set; }
    public int Rank { get; set; }
}

public class Badge : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
}

public class LearningPath : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
}
