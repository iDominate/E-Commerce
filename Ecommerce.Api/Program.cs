using Ecommerce.Api.MIddlewares;
using Ecommerce.Api.ServicesRegisteration.cs;
using Ecommerce.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
#region Logging 
Log.Logger = new LoggerConfiguration()
.WriteTo.Console()
.CreateLogger(); //Configure Logging
#endregion
if(builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddInfrastructureTesting();
} else
{
    builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("SqlServer")!);    
}
builder.Services.AddApplication();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddRateLimitingService();
builder.Services.AddHealthChecks();
builder.Services.AddEndpoints();



builder.Host.UseSerilog();
builder.Services.AddHttpLogging();
var app = builder.Build();
app.UseStaticFiles();
app.UseHttpLogging();
app.UseHealthChecks("/health");
app.UseExceptionHandler();
app.MapEndpoints();
app.Run();


