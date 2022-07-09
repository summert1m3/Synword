namespace Synword.PublicApi.UtilityEndpoints.MobileAppEndpoints.AppVersion;

public class MobileAppVersionResponse
{
    public MobileAppVersionResponse(string clientAppVersion)
    {
        ClientAppVersion = clientAppVersion;
    }
    public string ClientAppVersion { get; }
}
