using System;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Xunit;

namespace UnitTests.Domain.Entities.ValueObjects;

public class EmailTests
{

    [Fact]
    public void CreateEmail_EmailNotValid_Exception()
    {
        //Assert
        Assert.Throws<Exception>(() =>
        {
            Email email = new("test");
        });
    }
    
    [Fact]
    public void CreateEmail_EmailValid_Success()
    {
        //Assert
        string emailStr = "test@test.com";
        
        //Act
        Email email = new(emailStr);
        
        //Assert
        Assert.Equal(email.Value, emailStr);
    }
}
