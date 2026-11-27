using Castle.Core.Logging;
using Ecommerce.Infrastructure.Services.Caching;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ecommerce.Tests.UnitTests.Infrastructure.Services;

public sealed class MemoryCacheServiceTest
{
    private readonly MemoryCacheService _service;

    public MemoryCacheServiceTest()
    {
        var memoryCache = new MemoryCache(
            new MemoryCacheOptions());

            var logger = new Mock<ILogger<MemoryCacheService>>().Object;

        _service = new MemoryCacheService(
            memoryCache, logger);
    }
    [Fact]
    public async Task MemoryCacheService_ShouldBeInitialized()
    {
        // Arrange
        string outValue = "CacheValue";
        await _service.SetAsync("key", outValue, TimeSpan.FromMinutes(5));

        // Act
        var result = await _service.GetAsync<string>("key");

        // Assert
        _service.Should().NotBeNull();
        result.Should().NotBeNull().And.BeOfType<string>().And.Be(outValue);
    }

    [Fact]
    public async Task MemoryCacheService_ShouldReturnNull_WhenCacheMiss()
    {
        // Arrange
        object outValue = "CacheValue";

        // Act
        var result = await _service.GetAsync<string>("key");

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task MemoryCacheService_ShouldReturnValue_WhenSetMethodIsCalled()
    {
        // Arrange
        object outValue = "CacheValue";

        // Act
        await _service.SetAsync("key", outValue, TimeSpan.FromMinutes(5));

        // Assert
        var result = await _service.GetAsync<string>("key");
        result.Should().NotBeNull().And.BeOfType<string>().And.Be((string)outValue);
    }

    [Fact]
    public async Task MemoryCacheService_ShouldRemoveValue_WhenRemoveMethodIsCalled()
    {
        // Arrange
        object outValue = "CacheValue";

        // Act

        await _service.SetAsync("key", outValue, TimeSpan.FromMinutes(5));
        await _service.RemoveAsync("key");

        // Assert
        var result = await _service.GetAsync<string>("key");
        result.Should().BeNull();
    }
}