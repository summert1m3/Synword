using System.Collections.Generic;
using Synword.Application.Validation.RephraseValidation;
using Synword.Domain.Constants;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Synword.Domain.Enums;
using UnitTests.Utils;
using Xunit;

namespace UnitTests.Application.Validation;

public class RephraseRequestValidationTests
{
    private readonly RephraseRequestValidation _validation = new();
    private readonly User _correctUser = new User(
        id: DefaultUserInitialData.UserId,
        ip: new Ip(DefaultUserInitialData.Ip),
        roles: new List<Role>() {Role.Guest},
        usageData: new UsageData(
            plagiarismCheckCount: 0,
            rephraseCount: 0,
            lastVisitDate: DefaultUserInitialData.DateTimeNow,
            creationDate: DefaultUserInitialData.DateTimeNow
        ),
        coins: new Coins(ServicePricesConstants.RephrasePrice)
    );
    
    [Fact]
    public void IsValid_ValidData_True()
    {
        //Arrange

        //Act
        bool actual = _validation.IsValid(
            _correctUser, PreparedText.GetText445Chars(), ServicePricesConstants.RephrasePrice);

        //Assert
        Assert.True(actual);
    }

    [Fact]
    public void IsValid_TextLenghtUnderMinLimit_False()
    {
        //Arrange

        //Act
        bool actual = _validation.IsValid(
            _correctUser, PreparedText.GetText11Chars(), ServicePricesConstants.RephrasePrice);
        
        //Assert
        Assert.False(actual);
    }

    [Fact]
    public void IsValid_TextLenghtOverMaxLimit_False()
    {
        //Arrange

        //Act
        bool actual = _validation.IsValid(
            _correctUser, 
            PreparedText.GetText20470Chars(), 
            ServicePricesConstants.RephrasePrice);

        //Assert
        Assert.False(actual);
    }

    [Fact]
    public void IsValid_NotEnoughCoins_False()
    {
        //Arrange
        int userBalance = 0;
        User incorrectUser = new User(
            id: DefaultUserInitialData.UserId,
            ip: new Ip(DefaultUserInitialData.Ip),
            roles: new List<Role>() {Role.Guest},
            usageData: new UsageData(
                plagiarismCheckCount: 0,
                rephraseCount: 0,
                lastVisitDate: DefaultUserInitialData.DateTimeNow,
                creationDate: DefaultUserInitialData.DateTimeNow
            ),
            coins: new Coins(userBalance)
        );
        
        //Act
        bool actual = _validation.IsValid(
            incorrectUser, PreparedText.GetText445Chars(), userBalance + 1);
        
        //Assert
        Assert.False(actual);
    }
}
