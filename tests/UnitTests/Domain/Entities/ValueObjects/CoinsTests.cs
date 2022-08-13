using System;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Xunit;

namespace UnitTests.Domain.Entities.ValueObjects;

public class CoinsTests
{
    [Fact]
    public void CreateCoins_NegativeAmount_Exception()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Coins coins = new(-1);
        });
    }
}
