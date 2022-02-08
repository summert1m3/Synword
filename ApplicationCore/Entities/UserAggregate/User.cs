using Ardalis.GuardClauses;
using Synword.ApplicationCore.Interfaces;
using Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

namespace Synword.ApplicationCore.Entities.UserAggregate;

public class User : BaseEntity, IAggregateRoot
{
    public User(ExternalSignIn? externalSignIn, Ip ip, Role role, 
        Coins coins, UsageData usageData, History? history, Metadata? metadata)
    {
        Guard.Against.Null(ip, nameof(ip));
        Guard.Against.Null(role, nameof(role));
        Guard.Against.Null(coins, nameof(coins));
        Guard.Against.Null(usageData, nameof(usageData));
        
        ExternalSignIn = externalSignIn;
        Ip = ip;
        Role = role;
        Coins = coins;
        UsageData = usageData;
        History = history;
        Metadata = metadata;
    }

    public ExternalSignIn? ExternalSignIn { get; }
    public Ip Ip { get; }
    public Role Role { get; }
    public Coins Coins { get; }
    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
    public UsageData UsageData { get; }
    public History? History { get; }
    public Metadata? Metadata { get; }

    public void AddOrder(Order order)
    {
        Guard.Against.Null(order, nameof(order));
        
        _orders.Add(order);
    }
}
