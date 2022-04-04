namespace Synword.PublicApi.PriceListEndpoints;

public class AppServicePricesDTO
{
    public AppServicePricesDTO(string itemName, int price)
    {
        ItemName = itemName;
        Price = price;
    }
    public string ItemName { get; }
    public int Price { get; }
}
