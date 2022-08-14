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
        var response 
            =  await TestBase.NewClient.PostAsync("guestRegister", null);
        
        response.EnsureSuccessStatusCode();
        
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<GuestRegistrationDto>();
        TestBase.GuestRegistrationDto = model;
        
        Assert.IsTrue(Guid.TryParse(model.UserId, out Guid result));
    }
}
