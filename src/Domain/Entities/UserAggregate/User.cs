using Ardalis.GuardClauses;
using Synword.Domain.Constants;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Synword.Domain.Enums;
using Synword.Domain.Interfaces;

namespace Synword.Domain.Entities.UserAggregate;

public class User : BaseEntity<string>, IAggregateRoot
{
    private User()
    {
        // required by EF
    }

    public User(string id, Ip ip, List<Role> roles,
        Coins coins, UsageData usageData)
    {
        Guard.Against.Null(id, nameof(id));
        Guard.Against.Null(ip, nameof(ip));
        Guard.Against.Null(roles, nameof(roles));
        Guard.Against.Null(coins, nameof(coins));
        Guard.Against.Null(usageData, nameof(usageData));

        Id = id;
        Ip = ip;
        _roles = roles;
        Coins = coins;
        UsageData = usageData;
    }

    public new string Id { get; private set; }
    public ExternalSignIn? ExternalSignIn { get; private set; }
    public Ip Ip { get; private set; }
    private readonly List<Role> _roles = new();
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
    public Coins Coins { get; private set; }
    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
    public UsageData UsageData { get; private set; }

    private readonly List<PlagiarismCheckResult> _plagiarismCheckHistory = new();

    public IReadOnlyCollection<PlagiarismCheckResult> PlagiarismCheckHistory
        => _plagiarismCheckHistory.AsReadOnly();

    private readonly List<RephraseResult> _rephraseHistory = new();
    
    public IReadOnlyCollection<RephraseResult> RephraseHistory 
        => _rephraseHistory.AsReadOnly();
    
    public Metadata? Metadata { get; private set; }

    public void SpendCoins(int count)
    {
        Guard.Against.NegativeOrZero(count, nameof(count));
        
        if (Coins.Value < count)
        {
            throw new Exception("Coins.Value < count");
        }

        Coins = new Coins(Coins.Value - count);
    }

    public void IncreaseCoins(int count)
    {
        Guard.Against.NegativeOrZero(count, nameof(count));

        Coins = new Coins(Coins.Value + count);
    }
    
    public void RecordPlagiarismResultInHistory(PlagiarismCheckResult result)
    {
        Guard.Against.Null(result, nameof(result));
        
        _plagiarismCheckHistory.Add(result);
    }
    
    public void RecordRephraseResultInHistory(RephraseResult result)
    {
        Guard.Against.Null(result, nameof(result));
        
        _rephraseHistory.Add(result);
    }

    public void AddOrder(Order order)
    {
        Guard.Against.Null(order, nameof(order));

        _orders.Add(order);
    }

    public void AddExternalSignIn(ExternalSignIn externalSignIn)
    {
        Guard.Against.Null(externalSignIn, nameof(externalSignIn));

        if (ExternalSignIn != null)
        {
            throw new Exception("ExternalSignIn already exist");
        }

        ExternalSignIn = externalSignIn;
    }
    
    public void AddMetadata(Metadata metadata)
    {
        Guard.Against.Null(metadata, nameof(metadata));

        Metadata = metadata;
    }

    public static User CreateDefaultGuest(string id, string ip, DateTime dateTimeNow)
    {
        Guard.Against.NullOrEmpty(nameof(id), id);
        Guard.Against.NullOrEmpty(nameof(ip), ip);

        User guest = new User(
            id: id,
            ip: new Ip(ip),
            roles: new List<Role>() {Role.Guest},
            usageData: new UsageData(
                plagiarismCheckCount: 0,
                rephraseCount: 0,
                lastVisitDate: dateTimeNow,
                creationDate: dateTimeNow
            ),
            coins: new Coins(DefaultUserDataConstants.InitialCoinsCount)
        );

        return guest;
    }
}
