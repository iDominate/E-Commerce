using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
.WriteTo.Console()
.CreateLogger(); //Configure Logging
builder.Services.AddOptions<SqlServerOptions>()
.BindConfiguration("ConnectionStrings:SqlServer");
builder.Services.AddOptions<RedisOptions>()
.BindConfiguration(RedisOptions.SectionName);

var redisOptions = builder.Configuration.GetSection(RedisOptions.SectionName)
.Get<RedisOptions>(); 
builder.Services.AddHealthChecks().
AddSqlServer(builder.Configuration.GetConnectionString("SqlServer")!)
.AddRedis(
    redisOptions!.ToConfig().ToString(),
    "redis"
);
builder.Host.UseSerilog();
builder.Services.AddHttpLogging();
var app = builder.Build();
app.UseStaticFiles();
app.UseHttpLogging();
app.UseHealthChecks("/health");
app.MapGet("/", () => "Hello World");

app.Run();

