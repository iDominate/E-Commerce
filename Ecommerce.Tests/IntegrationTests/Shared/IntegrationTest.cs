using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Tests.IntegrationTests.Shared;

public abstract class IntegrationTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    protected readonly HttpClient _client;
    protected readonly AppDbContext _context;
    protected IntegrationTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();;
        var scope = factory.Services.CreateScope();
        this._context = scope.ServiceProvider.
        GetRequiredService<AppDbContext>();
    }

    public async Task DisposeAsync()
    {
        await ResetDataBaseAsync();
    }

    public async Task InitializeAsync()
    {
        await ResetDataBaseAsync();
    }

    public async Task ResetDataBaseAsync()
    {
        await this._context.Database.EnsureDeletedAsync();
        await this._context.Database.EnsureCreatedAsync();
    }
}