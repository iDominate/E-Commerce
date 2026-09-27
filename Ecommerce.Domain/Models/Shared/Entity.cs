/// <summary>
/// The base class for all entities
/// <param name="id">A unique identifier for the entity</param>
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected set; }
}