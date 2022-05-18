using Application.PlagiarismCheck.DTOs;
using Application.Rephrase.DTOs;
using Application.Users.DTOs;
using AutoMapper;
using MediatR;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;

namespace Application.Users.Queries;

public class GetAllUserHistories : IRequest<UserHistoriesDTO>
{
    public string UId { get; }
    
    public GetAllUserHistories(string uId)
    {
        UId = uId;
    }
}

internal class GetAllUserHistoriesHandler : 
    IRequestHandler<GetAllUserHistories, UserHistoriesDTO>
{
    private readonly ISynwordRepository<User>? _userRepository;
    private readonly IMapper _mapper;
    
    public GetAllUserHistoriesHandler(
        ISynwordRepository<User> userRepository, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserHistoriesDTO> Handle(
        GetAllUserHistories request, 
        CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(
            request.UId, cancellationToken);

        List<RephraseResultDTO> rephraseHistories = 
            _mapper.Map<List<RephraseResultDTO>>(
            user.RephraseHistory
        );
        
        List<PlagiarismCheckResultDTO> plagiarismCheckHistories = 
            _mapper.Map<List<PlagiarismCheckResultDTO>>(
                user.PlagiarismCheckHistory
            );
        
        return new UserHistoriesDTO()
        {
            rephraseHistories = rephraseHistories, 
            plagiarismCheckHistories = plagiarismCheckHistories
        };
    }
}
