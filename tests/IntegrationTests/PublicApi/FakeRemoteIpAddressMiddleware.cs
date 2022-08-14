using System.Net;
using Microsoft.AspNetCore.Http;

namespace IntegrationTests.PublicApi;

public class FakeRemoteIpAddressMiddleware
{
    private readonly RequestDelegate next;
    private readonly IPAddress fakeIpAddress = IPAddress.Parse("100.100.100.100");

    public FakeRemoteIpAddressMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Connection.RemoteIpAddress = fakeIpAddress;

        await this.next(httpContext);
    }
}
