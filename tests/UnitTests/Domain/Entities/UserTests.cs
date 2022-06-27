using System;
using System.Collections.Generic;
using System.Linq;
using Synword.Domain.Constants;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Synword.Domain.Enums;
using Synword.Domain.Extensions;
using Xunit;

namespace UnitTests.Domain.Entities;

public class UserTests
{
    private readonly string _userId = "id";
    private readonly string _ip = "100.100.100.100";
    private readonly DateTime _dateTimeNow = DateTime.Now;
    [Fact]
    public void CreateDefaultGuest_GuestValid_GuestEqual()
    {
        // Arrange
        User expected = new User(
            id: _userId,
            ip: new Ip(_ip),
            roles: new List<Role>() { Role.Guest },
            usageData: new UsageData(
                plagiarismCheckCount: 0,
                rephraseCount: 0,
                lastVisitDate: _dateTimeNow,
                creationDate: _dateTimeNow
            ),
            coins: new Coins(DefaultUserDataConstants.InitialCoinsCount)
        );
        
        // Act
        User actual = User.CreateDefaultGuest(_userId, _ip, _dateTimeNow);
        
        // Assert
        Assert.Equal(expected.ToJson(), actual.ToJson());
    }
    
    [Fact]
    public void AddExternalSignIn_ExternalSignInExist_ExternalSignInNotNull()
    {
        // Arrange
        User user = User.CreateDefaultGuest(_userId, _ip, _dateTimeNow);
        
        // Act
        
        user.AddExternalSignIn(
            new ExternalSignIn(ExternalSignInType.Google, "google"));
        
        // Assert
        Assert.NotNull(user.ExternalSignIn);
    }

    [Fact]
    public void AddOrder_OrderExist_OrderNotNull()
    {
        // Arrange
        User user = User.CreateDefaultGuest(_userId, _ip, _dateTimeNow);
        
        //Act
        user.AddOrder(
            new Order("test", "test", _dateTimeNow));
        
        //Assert
        Assert.NotNull(user.Orders.First());
    }
}
