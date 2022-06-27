namespace Synword.PublicApi.AppVersionEndpoints;

public class ClientAppVersionResponse
{
    public ClientAppVersionResponse(string clientAppVersion)
    {
        ClientAppVersion = clientAppVersion;
    }
    public string ClientAppVersion { get; }
}
