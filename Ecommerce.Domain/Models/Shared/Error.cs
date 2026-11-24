/// <summary>
/// The base class for all errors
/// </summary>
/// <param name="Code">A short identifier for the error</param>
/// <param name="Description">A description for the error</param>
/// <param name="ErrorType">The type of the error</param>
public sealed record Error(string Code, string Description, ErrorType ErrorType)
{
}