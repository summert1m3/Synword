using Synword.Domain.Interfaces;

namespace Synword.Domain.Entities.Identity.Token;

public class RefreshToken : BaseEntity
{
    public RefreshToken(
        string token,
        string deviceId,
        DateTime expires,
        DateTime created
        )
    {
        Token = token;
        DeviceId = deviceId;
        Expires = expires;
        Created = created;
    }

    public string DeviceId { get; private set; }
    public string Token { get; private set; }
    public DateTime Expires { get; private set; }
    public DateTime Created { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
}
