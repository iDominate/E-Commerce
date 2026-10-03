using Microsoft.AspNetCore.RateLimiting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
#region Logging 
Log.Logger = new LoggerConfiguration()
.WriteTo.Console()
.CreateLogger(); //Configure Logging
#endregion
#region IOptions pattern
builder.Services.AddOptions<SqlServerOptions>()
.BindConfiguration("ConnectionStrings:SqlServer");
builder.Services.AddOptions<RedisOptions>()
.BindConfiguration(RedisOptions.SectionName);
#endregion
#region Rate limiting
builder.Services.AddRateLimiter(o =>
{
    o.AddSlidingWindowLimiter("api", configureOptions =>
    {
        configureOptions.PermitLimit = 100;
        configureOptions.Window =TimeSpan.FromMinutes(1);
        configureOptions.SegmentsPerWindow = 6;
        configureOptions.QueueLimit = 0;
    });
    o.AddFixedWindowLimiter("auth", configureOptions =>
    {
        configureOptions.PermitLimit = 5;
        configureOptions.Window = TimeSpan.FromMinutes(1);
        configureOptions.QueueLimit = 0;
    });
});
#endregion
#region Health checks
var redisOptions = builder.Configuration.GetSection(RedisOptions.SectionName)
.Get<RedisOptions>(); 
builder.Services.AddHealthChecks().
AddSqlServer(builder.Configuration.GetConnectionString("SqlServer")!)
.AddRedis(
    redisOptions!.ToConfig().ToString(),
    "redis"
);
#endregion
builder.Host.UseSerilog();
builder.Services.AddHttpLogging();
var app = builder.Build();
app.UseStaticFiles();
app.UseHttpLogging();
app.UseHealthChecks("/health");
app.MapGet("/", () => "Hello World");

app.Run();


