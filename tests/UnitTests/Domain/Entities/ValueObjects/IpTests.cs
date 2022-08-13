using System;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Xunit;

namespace UnitTests.Domain.Entities.ValueObjects;

public class IpTests
{
    [Fact]
    public void CreateIp_IpNotValid_Exception()
    {
        Assert.Throws<FormatException>(() =>
        {
            Ip ip = new Ip("123");
        });
    }
    
    [Fact]
    public void CreateIp_IpValid_Success()
    {
        //Arrange
        string ipStr = "100.100.100.100";
        
        //Act
        Ip ip = new(ipStr);
        
        //Assert
        Assert.Equal(ip.Value, ipStr);
    }
}
