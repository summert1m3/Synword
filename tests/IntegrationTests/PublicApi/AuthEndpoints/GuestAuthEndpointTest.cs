using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synword.Application.Guests.DTOs;
using Synword.Domain.Extensions;

namespace IntegrationTests.PublicApi.AuthEndpoints;

[TestClass]
public class GuestAuthEndpointTest
{
    [TestMethod]
    public async Task GuestAuth_Valid_Success()
    {
        Dictionary<string, string> values =
            new Dictionary<string, string> 
            {
                {"userId", TestBase.GuestRegistrationDto.UserId},
            };

        var response = await TestBase.NewClient.PostAsync(
            "guestAuth", new FormUrlEncodedContent(values));
        
        response.EnsureSuccessStatusCode();
        
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<GuestAuthenticateDto>();

        TestBase.GuestAuthenticateDto = model;
    }
}
