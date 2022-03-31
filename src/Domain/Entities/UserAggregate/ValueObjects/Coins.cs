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
        Guard.Against.OutOfRange(coins, nameof(coins), 0, int.MaxValue);

        Value = coins;
    }
    public int Value { get; private set; }
}
