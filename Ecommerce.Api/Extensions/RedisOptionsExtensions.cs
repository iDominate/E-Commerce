using StackExchange.Redis;

public static class RedisOptionsExtensions
{
    public static ConfigurationOptions ToConfig(this RedisOptions redisOptions)
    {
        return new ConfigurationOptions
        {
            EndPoints =
            {
                {redisOptions.Host, redisOptions.Port}
            },
            Password = redisOptions.Password,
            DefaultDatabase = redisOptions.Database,
            Ssl = redisOptions.Ssl
        };
    }    
}