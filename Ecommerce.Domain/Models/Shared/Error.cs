/// <summary>
/// The base class for all errors
/// </summary>
/// <param name="Code">A short identifier for the error</param>
/// <param name="Descitpion">A description for the error</param>
/// <param name="ErrorType">The type of the error</param>
public sealed record Error(string Code, string Descitpion, ErrorType ErrorType)
{
    public Error None => new(string.Empty, string.Empty, ErrorType.None); 

    /// <summary>
    /// Field indicating that no errors occurred
    /// </summary>
    public bool isNone => ErrorType == ErrorType.None;
}