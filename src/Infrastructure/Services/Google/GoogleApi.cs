// ReSharper disable RedundantUsingDirective
using System.Net;
using Synword.Domain.Extensions;

namespace Synword.Infrastructure.Services.Google;

public class GoogleApi : IGoogleApi
{
    private const string GoogleApiTokenInfoUrl 
        = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={0}";

    public async Task<GoogleUserModel> GetGoogleUserData(string accessToken)
    {
#if !DEBUG
        HttpClient httpClient = new HttpClient();
        var requestUri = new Uri(string.Format(GoogleApiTokenInfoUrl, accessToken));

        HttpResponseMessage httpResponseMessage;
        try 
        {
            httpResponseMessage = await httpClient.GetAsync(requestUri);
        } 
        catch (Exception ex) 
        {
            throw new Exception(ex.Message);
        }

        if (httpResponseMessage.StatusCode != HttpStatusCode.OK) 
        {
            throw new Exception("Invalid access token");
        }
        
        var response = httpResponseMessage.Content.ReadAsStringAsync().Result;

        var googleUserModel = response.FromJson<GoogleUserModel>();

        return googleUserModel;
#endif
        
#if DEBUG
        return new GoogleUserModel()
        {
            Id = accessToken
        };
#endif
    }
}
