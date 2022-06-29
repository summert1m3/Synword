using MinimalApi.Endpoint;
using Synword.Domain.Constants;

namespace Synword.PublicApi.PriceListEndpoints;

public class PriceListEndpoint : IEndpoint<IResult>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("priceList", 
            async () => await HandleAsync()
            ).Produces<PriceListResponse>();
    }

    public async Task<IResult> HandleAsync()
    {
        List<AppServicePricesDTO> prices = new()
        {
            new AppServicePricesDTO(
                nameof(ServicePricesConstants.PlagiarismCheckPrice), 
                ServicePricesConstants.PlagiarismCheckPrice), 
            new AppServicePricesDTO(
                nameof(ServicePricesConstants.RephrasePrice), 
                ServicePricesConstants.RephrasePrice)
        };
        
        return Results.Ok(new PriceListResponse(prices));
    }
}
