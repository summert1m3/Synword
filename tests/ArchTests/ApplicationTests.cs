using NetArchTest.Rules;
using Synword.Application.AutoMapper;
using Xunit;

namespace ArchTests;

public class ApplicationTests
{
    [Fact]
    public void ApplicationShouldNotHaveDependencyOnInfrastructure()
    {
        var assembly = typeof(DomainProfile).Assembly;
        var result = Types.InAssembly(typeof(DomainProfile).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Synword.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ApplicationShouldNotHaveDependencyOnPersistence()
    {
        var result = Types.InAssembly(typeof(DomainProfile).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Synword.Persistence")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
