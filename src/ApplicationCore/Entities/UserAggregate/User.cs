using Ardalis.GuardClauses;
using Synword.ApplicationCore.Interfaces;
using Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;

namespace Synword.ApplicationCore.Entities.UserAggregate;

public class User : BaseEntity, IAggregateRoot
{
    private User()
    {
        // required by EF
    }
    
    public User(ExternalSignIn? externalSignIn, Ip ip, Role role, 
        Coins coins, UsageData usageData, Metadata? metadata)
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
        Metadata = metadata;
    }

    public ExternalSignIn? ExternalSignIn { get; private set; }
    public Ip Ip { get; private set; }
    public Role Role { get; private set; }
    public Coins Coins { get; private set; }
    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
    public UsageData UsageData { get; private set; }
    public List<PlagiarismCheckHistory>? PlagiarismCheckHistory { get; private set; }
    public List<RephraseHistory>? RephraseHistory { get; private set; }
    public Metadata? Metadata { get; private set; }

    public void AddOrder(Order order)
    {
        Guard.Against.Null(order, nameof(order));
        
        _orders.Add(order);
    }
}
