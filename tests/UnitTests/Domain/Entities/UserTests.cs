using System;
using System.Collections.Generic;
using System.Linq;
using Synword.Domain.Constants;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Synword.Domain.Enums;
using Synword.Domain.Extensions;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Domain.Entities;

public class UserTests
{
    private readonly Func<User> _createDefaultUser;

    public UserTests()
    {
        _createDefaultUser = () => User.CreateDefaultGuest
        (DefaultUserInitialData.UserId,
            DefaultUserInitialData.Ip,
            DefaultUserInitialData.DateTimeNow);
    }
    
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
    public void AddOrder_OrderExist_OrderNotNull()
    {
        // Arrange
        User user = _createDefaultUser.Invoke();

        //Act
        user.AddOrder(
            new Order("test", "test", 
                DefaultUserInitialData.DateTimeNow));

        //Assert
        Assert.NotNull(user.Orders.First());
    }

    [Fact]
    public void SpendCoins_AmountHigherThanBalance_Exception()
    {
        //Arrange
        User user = _createDefaultUser.Invoke();
        
        //Act
        Action spendCoins = () =>
        {
            user.SpendCoins(InitialUserDataConstants.InitialCoinsCount + 1);
        };
        
        //Assert
        Assert.Throws<Exception>(spendCoins);
    }

    [Fact]
    public void SpendCoins_Spend_CoinsDecreased()
    {
        //Arrange
        User user = _createDefaultUser.Invoke();
        int amountToSpend = 1;
        
        //Act
        user.SpendCoins(amountToSpend);
        
        //Assert
        Assert.Equal(
            user.Coins.Value, 
            InitialUserDataConstants.InitialCoinsCount - 1);
    }

    [Fact]
    public void RecordRephraseResult_AddResult_NotEmpty()
    {
        //Arrange
        User user = _createDefaultUser.Invoke();
        
        //Act
        user.RecordRephraseResultInHistory(PreparedResults.GetRephraseResult());
        
        //Assert
        Assert.NotEmpty(user.RephraseHistory);
    }

    [Fact]
    public void RecordPlagiarismResult_AddResult_NotEmpty()
    {
        //Arrange
        User user = _createDefaultUser.Invoke();
        
        //Act
        user.RecordPlagiarismResultInHistory(
            PreparedResults.GetPlagiarismCheckResult());
        
        //Assert
        Assert.NotEmpty(user.PlagiarismCheckHistory);
    }
}
