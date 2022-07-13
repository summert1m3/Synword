using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Synword.Application.AppFeatures.PlagiarismCheck.DTOs;
using Synword.Application.AppFeatures.Rephrase.DTOs.RephraseResult;
using Synword.Application.Users.DTOs;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Specifications;

namespace Synword.Application.Users.Queries;

public class GetAllUserHistoriesQuery : IRequest<UserHistoriesDto>
{
    public string UId { get; }
    
    public GetAllUserHistoriesQuery(string uId)
    {
        UId = uId;
    }
}

internal class GetAllUserHistoriesQueryHandler : 
    IRequestHandler<GetAllUserHistoriesQuery, UserHistoriesDto>
{
    private readonly ISynwordRepository<User> _userRepository;
    private readonly IMapper _mapper;
    
    public GetAllUserHistoriesQueryHandler(
        ISynwordRepository<User> userRepository, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserHistoriesDto> Handle(
        GetAllUserHistoriesQuery request, 
        CancellationToken cancellationToken)
    {
        var spec = new UserWithAllHistoriesSpecification(request.UId);
        User? user = await _userRepository.GetBySpecAsync(spec, cancellationToken);

        Guard.Against.Null(user, nameof(user));
        
        List<RephraseResultDto> rephraseHistories = 
            _mapper.Map<List<RephraseResultDto>>(
            user.RephraseHistory
        );
        
        List<PlagiarismCheckResultDto> plagiarismCheckHistories = 
            _mapper.Map<List<PlagiarismCheckResultDto>>(
                user.PlagiarismCheckHistory
            );
        
        return new UserHistoriesDto()
        {
            RephraseHistories = rephraseHistories, 
            PlagiarismCheckHistories = plagiarismCheckHistories
        };
    }
}
