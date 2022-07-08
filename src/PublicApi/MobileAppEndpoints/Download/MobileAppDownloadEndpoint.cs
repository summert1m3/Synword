using MinimalApi.Endpoint;
using Swashbuckle.AspNetCore.Annotations;

namespace Synword.PublicApi.MobileAppEndpoints.Download;

public class MobileAppDownloadEndpoint : IEndpoint<IResult>
{
    private string _filePath;

    public MobileAppDownloadEndpoint(IWebHostEnvironment webHostEnvironment)
    {
        char s = Path.DirectorySeparatorChar;
        _filePath = webHostEnvironment.ContentRootPath 
                    + $"Files{s}MobileApp{s}app.apk";
    }
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("downloadMobileApp", 
                [SwaggerOperation(
                    Tags = new[] { "Utility" }
                )]
                async (IConfiguration configuration) => await HandleAsync())
            .Produces<IResult>();
    }
    
    public async Task<IResult> HandleAsync()
    {
        string mimeType = "application/vnd.android.package-archive";
        FileStream stream = File.OpenRead(_filePath);
        Directory.GetCurrentDirectory();
        return Results.File(
            stream, mimeType, 
            fileDownloadName: "app.apk");
    }
}
