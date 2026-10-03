public class RedisOptions
{
    public const string SectionName = "Redis";
    public string Host { get; init; } = string.Empty;
    public int Port { get; init; } = 6379;
    public string? Password { get; init; }
    public int Database { get; init; } = 0;
    public bool Ssl { get; init; }
}