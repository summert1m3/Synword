using NetArchTest.Rules;
using Synword.Domain.Entities;
using Xunit;

namespace ArchTests;

public class DomainTests
{
    [Fact]
    public void DomainShouldNotHaveDependencyOnApplication()
    {
        var result = Types.InAssembly(typeof(BaseEntity).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Synword.Application")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DomainShouldNotHaveDependencyOnInfrastructure()
    {
        var result = Types.InAssembly(typeof(BaseEntity).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Synword.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DomainShouldNotHaveDependencyOnPersistence()
    {
        var result = Types.InAssembly(typeof(BaseEntity).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Synword.Persistence")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
