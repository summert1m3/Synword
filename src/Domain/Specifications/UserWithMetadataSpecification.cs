using Ardalis.Specification;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Specifications;

public sealed class UserWithMetadataSpecification : Specification<User>, ISingleResultSpecification
{
    public UserWithMetadataSpecification(string uId)
    {
        Query.Where(u => u.Id == uId).Include(u => u.Metadata);
    }
}
