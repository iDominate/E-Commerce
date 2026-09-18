using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
.WriteTo.Console()
.CreateLogger();
builder.Host.UseSerilog();
var app = builder.Build();
app.UseHttpLogging();
app.Run();

