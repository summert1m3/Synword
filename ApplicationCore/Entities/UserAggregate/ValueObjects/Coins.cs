using Ardalis.GuardClauses;

namespace Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

public class Coins
{
    public Coins(int coins)
    {
        Guard.Against.Negative(coins, nameof(coins));
        Guard.Against.OutOfRange(coins, nameof(coins), 0, int.MaxValue);
    }
    public int Value { get; }
}
