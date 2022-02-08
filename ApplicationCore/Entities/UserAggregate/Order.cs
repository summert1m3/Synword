using Ardalis.GuardClauses;

namespace Synword.ApplicationCore.Entities.UserAggregate;

public class Order : BaseEntity
{
    public Order(string productId, string purchaseToken, DateTime orderDate)
    {
        Guard.Against.NullOrEmpty(productId, nameof(productId));
        Guard.Against.NullOrEmpty(purchaseToken, nameof(purchaseToken));
        Guard.Against.Null(orderDate, nameof(orderDate));
        
        ProductId = productId;
        PurchaseToken = purchaseToken;
        OrderDate = orderDate;
    }
    public string ProductId { get; }
    public string PurchaseToken { get; }
    public DateTime OrderDate { get; }
}
