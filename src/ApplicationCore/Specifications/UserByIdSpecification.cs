using Ardalis.Specification;
using Synword.ApplicationCore.Entities.UserAggregate;

namespace Synword.ApplicationCore.Specifications;

public class UserByIdSpecification : Specification<User>, ISingleResultSpecification
{
    public UserByIdSpecification(string ip)
    {
        
    }
}
