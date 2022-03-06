using MinimalApi.Endpoint;

namespace Synword.PublicApi.PriceListEndpoints;

public class PriceListEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/priceList", async () =>
        {
            
        }).Produces<PriceListResponse>();
    }
}
