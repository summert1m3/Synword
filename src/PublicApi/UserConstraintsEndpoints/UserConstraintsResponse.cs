using Synword.Domain.Constants;

namespace Synword.PublicApi.UserConstraintsEndpoints;

public class UserConstraintsResponse
{
    public IServiceConstraints Default { get; init; }
    public IServiceConstraints Silver { get; init; }
    public IServiceConstraints Gold { get; init; }
}
