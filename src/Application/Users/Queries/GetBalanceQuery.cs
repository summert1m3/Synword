using Application.Users.DTOs;
using Ardalis.GuardClauses;
using MediatR;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;

namespace Application.Users.Queries;

public class GetBalanceQuery : IRequest<BalanceDTO>
{
    public string UId { get; }

    public GetBalanceQuery(string uId)
    {
        UId = uId;
    }
}

internal class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, BalanceDTO>
{
    private readonly ISynwordRepository<User> _userRepository;

    public GetBalanceQueryHandler(
        ISynwordRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BalanceDTO> Handle(
        GetBalanceQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(
            request.UId, cancellationToken);

        Guard.Against.Null(user, nameof(user));

        return new BalanceDTO {Coins = user.Coins.Value};
    }
}
