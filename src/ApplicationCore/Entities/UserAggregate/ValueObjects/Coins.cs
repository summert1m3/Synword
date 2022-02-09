using Ardalis.GuardClauses;

namespace Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

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
    }
    public int Value { get; private set; }
}
