using Ardalis.Specification;
using Synword.ApplicationCore.Entities.UserAggregate;

namespace Synword.ApplicationCore.Specifications;

public class UserByExternalIdSpecification : Specification<User>, ISingleResultSpecification
{
    public UserByExternalIdSpecification(string externalId)
    {
        Query
            .Where(u => u.ExternalSignIn.Id == externalId);
    }
}
