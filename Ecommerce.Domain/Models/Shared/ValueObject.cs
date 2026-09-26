public abstract record ValueObjet<T>
{
    protected ValueObjet(T value)
    {
        Value = value;
    }
    public abstract ValueObjet<T> CreateInstance(string value);
    public T Value { get; private set;}
}