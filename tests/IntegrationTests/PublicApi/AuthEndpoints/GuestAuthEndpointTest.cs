using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests.PublicApi.AuthEndpoints;

[TestClass]
public class GuestAuthEndpointTest
{
    [TestMethod]
    public async Task GuestAuth_Valid_Success()
    {
        var model = await TestBase.GuestRegisterAndAuth();
        
        Assert.IsNotNull(model);
    }
}
