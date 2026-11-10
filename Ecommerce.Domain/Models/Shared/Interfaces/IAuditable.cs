
/// <summary>
/// A contract for dates
/// </summary>
public interface IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}
}