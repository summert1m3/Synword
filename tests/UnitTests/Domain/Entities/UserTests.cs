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
    [Fact]
    public void CreateDefaultGuest_GuestValid_GuestEqual()
    {
        // Arrange
        User expected = new User(
            id: DefaultUserInitialData.UserId,
            ip: new Ip(DefaultUserInitialData.Ip),
            roles: new List<Role> {Role.Guest},
            usageData: new UsageData(
                plagiarismCheckCount: 0,
                rephraseCount: 0,
                lastVisitDate: DefaultUserInitialData.DateTimeNow,
                creationDate: DefaultUserInitialData.DateTimeNow
            ),
            coins: new Coins(InitialUserDataConstants.InitialCoinsCount)
        );

        // Act
        User actual = User.CreateDefaultGuest
        (DefaultUserInitialData.UserId,
            DefaultUserInitialData.Ip,
            DefaultUserInitialData.DateTimeNow);

        // Assert
        Assert.Equal(expected.ToJson(), actual.ToJson());
    }

    [Fact]
    public void AddExternalSignIn_ExternalSignInExist_ExternalSignInNotNull()
    {
        // Arrange
        User user = User.CreateDefaultGuest
        (DefaultUserInitialData.UserId,
            DefaultUserInitialData.Ip,
            DefaultUserInitialData.DateTimeNow);

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
        User user = User.CreateDefaultGuest
        (DefaultUserInitialData.UserId,
            DefaultUserInitialData.Ip,
            DefaultUserInitialData.DateTimeNow);

        //Act
        user.AddOrder(
            new Order("test", "test", 
                DefaultUserInitialData.DateTimeNow));

        //Assert
        Assert.NotNull(user.Orders.First());
    }
}
