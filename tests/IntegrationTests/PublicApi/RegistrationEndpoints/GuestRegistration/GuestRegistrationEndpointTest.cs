using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.PublicApi.RegistrationEndpoints.GuestRegistration;

[TestClass]
public class GuestRegistrationEndpointTest
{
    [TestMethod]
    public async Task GuestRegister_Valid_Success()
    {
        var model = await TestBase.GuestRegisterRequest();

        Assert.IsTrue(Guid.TryParse(model.UserId, out Guid result));
    }
}
