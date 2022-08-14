using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synword.Application.Guests.DTOs;
using Synword.Domain.Extensions;

namespace IntegrationTests.PublicApi;

[TestClass]
public class TestBase
{
    private static WebApplicationFactory<Program> _application;

    public static async Task<GuestAuthenticateDto> GuestRegisterAndAuth()
    {
        var registrationResponse = await GuestRegisterRequest();
        
        return await GuestAuthRequest(registrationResponse.UserId);
    }

    public static async Task<GuestRegistrationDto> GuestRegisterRequest()
    {
        var response 
            =  await NewClient.PostAsync("guestRegister", null);
        
        response.EnsureSuccessStatusCode();
        
        var stringResponse = await response.Content.ReadAsStringAsync();

        return stringResponse.FromJson<GuestRegistrationDto>();
    }

    public static async Task<GuestAuthenticateDto> GuestAuthRequest(string uId)
    {
        Dictionary<string, string> values =
            new Dictionary<string, string> 
            {
                {"userId", uId},
            };

        var response = await NewClient.PostAsync(
            "guestAuth", new FormUrlEncodedContent(values));
        
        response.EnsureSuccessStatusCode();
        
        var stringResponse = await response.Content.ReadAsStringAsync();
        return stringResponse.FromJson<GuestAuthenticateDto>();
    }

    public static HttpClient NewClient
    {
        get
        {
            return _application.CreateClient();
        }
    }

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext _)
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(ConfigureServices);
                });
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IStartupFilter, StartupFilter>();
    }
}
