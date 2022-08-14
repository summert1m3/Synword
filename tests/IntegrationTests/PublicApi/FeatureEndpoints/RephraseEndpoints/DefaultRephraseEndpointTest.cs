using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synword.Application.AppFeatures.Rephrase.DTOs.RephraseResult;
using Synword.Domain.Extensions;

namespace IntegrationTests.PublicApi.FeatureEndpoints.RephraseEndpoints;

[TestClass]
public class DefaultRephraseEndpointTest
{
    [TestMethod]
    public async Task Rephrase_ValidInput_Success()
    {
        var guestAuthModel = await TestBase.GuestRegisterAndAuth();
        
        string input = "Bitcoin is a decentralized digital currency " +
                       "that can be transferred on the peer-to-peer bitcoin network.";
        
        var request = new HttpRequestMessage() 
            {RequestUri = new Uri("http://localhost:5000/rephrase"), Method = HttpMethod.Post};

        request.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer", 
                guestAuthModel.AccessToken);

        Dictionary<string, string> values =
            new Dictionary<string, string> 
            {
                {"text", input},
                {"language", "eng"}
            };
        
        request.Content = new FormUrlEncodedContent(values);

        var response = await TestBase.NewClient.SendAsync(request);
        
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<RephraseResultDto>();

        var expected = "Bitcoin is a decentralized computerized cash that can be exchanged on the peer-to-peer bitcoin organize.";

        Assert.AreEqual(model.RephrasedText, expected);
    }
}
