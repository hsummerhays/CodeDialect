namespace CodeDialect.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime Created { get; init; } = DateTime.UtcNow;
    public string? CreatedBy { get; init; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
