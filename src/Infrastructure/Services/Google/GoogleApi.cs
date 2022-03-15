using System.Net;
using Synword.ApplicationCore;

namespace Synword.Infrastructure.Services.Google;

public class GoogleApi : IGoogleApi
{
    private const string GoogleApiTokenInfoUrl 
        = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={0}";

    public GoogleUserModel GetGoogleUserData(string accessToken)
    {
        HttpClient httpClient = new HttpClient();
        var requestUri = new Uri(string.Format(GoogleApiTokenInfoUrl, accessToken));

        HttpResponseMessage httpResponseMessage;
        try 
        {
            httpResponseMessage = httpClient.GetAsync(requestUri).Result;
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
    }
}
