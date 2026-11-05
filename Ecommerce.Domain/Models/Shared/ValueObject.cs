using Ecommerce.Domain.Models.Shared;

/// <summary>
/// A base class for all value objects
/// </summary>
/// <typeparam name="T">The uderlying type of the value object</typeparam>
public abstract record ValueObjet<TSelf, TValue>
{
    protected ValueObjet(TValue value)
    {
        Value = value;
    }


    /// <summary>
    /// The underlying value
    /// </summary>
    public TValue Value { get; private set;}
}