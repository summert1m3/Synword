using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace IntegrationTests.PublicApi;

public class StartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(
        Action<IApplicationBuilder> next)
    {
        return builder =>
        {
            builder.UseMiddleware<FakeRemoteIpAddressMiddleware>();
            next(builder);
        };
    }
}
