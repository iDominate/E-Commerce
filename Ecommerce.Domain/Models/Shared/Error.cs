public sealed record Error(string Code, string Descitpion, ErrorType ErrorType)
{
    public Error None => new(string.Empty, string.Empty, ErrorType.None); 
    public bool isNone => ErrorType == ErrorType.None;
}