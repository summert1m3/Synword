using Ardalis.GuardClauses;

namespace Synword.Domain.Entities.UserAggregate.ValueObjects;

public class Coins
{
    private Coins()
    {
        // required by EF
    }
    
    public Coins(int coins)
    {
        Guard.Against.Negative(coins, nameof(coins));

        Value = coins;
    }
    public int Value { get; private set; }
}
