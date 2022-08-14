using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synword.Application.Guests.DTOs;
using Synword.Domain.Extensions;

namespace IntegrationTests.PublicApi.RegistrationEndpoints.GuestRegistration;

[TestClass]
public class GuestRegistrationEndpointTest
{
    [TestMethod]
    public async Task GuestRegister_Valid_Success()
    {
        HttpClient client = new HttpClient();
        
        var response 
            =  await TestBase.NewClient.PostAsync("guestRegister", null);
        
        response.EnsureSuccessStatusCode();
        
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<GuestRegistrationDto>();
        
        Assert.IsTrue(Guid.TryParse(model.UserId, out Guid result));
    }
}
