
/// <summary>
/// A contract for dates
/// </summary>
public interface IAuditable
{
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set;}
}