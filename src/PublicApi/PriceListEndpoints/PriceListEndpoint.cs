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
                nameof(AppServicePricesConstants.PlagiarismCheckPrice), 
                AppServicePricesConstants.PlagiarismCheckPrice), 
            new AppServicePricesDTO(
                nameof(AppServicePricesConstants.RephrasePrice), 
                AppServicePricesConstants.RephrasePrice)
        };
        
        return Results.Ok(new PriceListResponse(prices));
    }
}
