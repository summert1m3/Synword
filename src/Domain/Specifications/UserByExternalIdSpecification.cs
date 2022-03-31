using Ardalis.Specification;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Specifications;

public class UserByExternalIdSpecification : Specification<User>, ISingleResultSpecification
{
    public UserByExternalIdSpecification(string externalId)
    {
        Query
            .Where(u => u.ExternalSignIn.Id == externalId);
    }
}
