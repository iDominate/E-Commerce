using System.Runtime.CompilerServices;
using FluentAssertions;
using NetArchTest.Rules;

namespace Ecommerce.Tests.ArchitecturalTests;


public class ArchTests
{
    private const string _applicationLayer = "Ecommerce.Application";
    private const string _presentationLayer = "Ecommerce.Presentation";
    private const string _infrastructureLayer = "Ecommerce.Infrastructure";
    private const string _apiLayer = "Ecommerce.Api";
    private const string _domainLayer = "Ecommerce.Domain";

    [Fact]
    public void DomainLayer_ShouldNot_DependOn_OtherLayers()
    {
        //Act
        var result = Types.InCurrentDomain()
        .That()
        .ResideInNamespace(_domainLayer)
        .ShouldNot()
        .HaveDependencyOnAll(
            new string[]{_applicationLayer, _presentationLayer, _apiLayer, 
            _infrastructureLayer})
            .GetResult()
            .IsSuccessful;
        
        //Assert
        result.Should().BeTrue();

    }

    [Fact]
    public void InfrastructureLayer_Should_DependOn_DomainLayer()
    {
        //Act
        var result = Types.InCurrentDomain()
        .That()
        .ResideInNamespace(_infrastructureLayer)
        .Should()
        .HaveDependencyOn(_domainLayer)
        .GetResult()
        .IsSuccessful;

        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public void ApplicationLayer_should_DependOn_InfrastructureLayer()
    {
        
        //Act
        var result = Types.InCurrentDomain()
        .That()
        .ResideInNamespace(_applicationLayer)
        .Should()
        .HaveDependencyOn(_infrastructureLayer)
        .GetResult()
        .IsSuccessful;

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void PresentationLayer_Should_DependOn_ApplicationLayer()
    {
        
        //Act
        var result = Types.InCurrentDomain()
        .That()
        .ResideInNamespace(_presentationLayer)
        .Should()
        .HaveDependencyOn(_applicationLayer)
        .GetResult()
        .IsSuccessful;

        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public void ApiLayer_Shoud_DependOn_PresentationLayer()
    {

        //Act
        var result = Types.InCurrentDomain()
        .That()
        .ResideInNamespace(_apiLayer)
        .Should()
        .HaveDependencyOn(_presentationLayer)
        .GetResult()
        .IsSuccessful;

        //Assert
        result.Should().BeTrue();
    }
}