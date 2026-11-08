public sealed class AppValidationException : Exception
{
    public IReadOnlyCollection<Error> Errors { get; }

    public AppValidationException(
        IEnumerable<Error> errors)
    {
        Errors = errors.ToList();
    }
}