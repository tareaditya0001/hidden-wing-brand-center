namespace HiddenWing.BrandCenter.Domain.Entities;

/// <summary>
/// Shared identity and audit timestamps for persistable records.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
