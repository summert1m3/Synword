namespace Synword.PublicApi.PriceListEndpoints;

public class PriceListResponse
{
    public PriceListResponse(List<AppServicePricesDTO> prices)
    {
        Prices = prices;
    }
    
    public List<AppServicePricesDTO> Prices { get; }
}
