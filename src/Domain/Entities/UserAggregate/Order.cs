using Ardalis.GuardClauses;

namespace Synword.Domain.Entities.UserAggregate;

public class Order : BaseEntity
{
    private Order()
    {
        // required by EF
    }
    
    public Order(string productId, string purchaseToken, DateTime orderDate)
    {
        Guard.Against.NullOrEmpty(productId, nameof(productId));
        Guard.Against.NullOrEmpty(purchaseToken, nameof(purchaseToken));
        Guard.Against.Null(orderDate, nameof(orderDate));
        
        ProductId = productId;
        PurchaseToken = purchaseToken;
        OrderDate = orderDate;
    }
    public string ProductId { get; private set; }
    public string PurchaseToken { get; private set; }
    public DateTime OrderDate { get; private set; }
}
