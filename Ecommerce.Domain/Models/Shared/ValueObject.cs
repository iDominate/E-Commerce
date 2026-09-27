/// <summary>
/// A base class for all value objects
/// </summary>
/// <typeparam name="T">The uderlying type of the value object</typeparam>
public abstract record ValueObjet<T>
{
    protected ValueObjet(T value)
    {
        Value = value;
    }

    /// <summary>
    /// A method for creating an instance of a generic type
    /// </summary>
    /// <param name="value">the duerlying value</param>
    /// <returns>A new ValueObject of type T</returns>
    public abstract ValueObjet<T> CreateInstance(T value);

    /// <summary>
    /// The underlying value
    /// </summary>
    public T Value { get; private set;}
}