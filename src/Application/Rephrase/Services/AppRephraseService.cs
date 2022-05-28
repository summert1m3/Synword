using Application.Rephrase.DTOs;
using AutoMapper;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;
using Synword.Infrastructure.SynonymDictionary.EngSynonymDictionary.Queries;
using Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary.Queries;

namespace Application.Rephrase;

public class AppRephraseService : IAppRephraseService
{
    private readonly IMapper _mapper;
    private readonly IRephraseService _rephraseService;
    private readonly ISynwordRepository<User>? _userRepository;
    
    public AppRephraseService(
        IMapper mapper, 
        IRephraseService rephraseService,
        ISynwordRepository<User>? userRepository)
    {
        _mapper = mapper;
        _rephraseService = rephraseService;
        _userRepository = userRepository;
    }
    
    public async Task<RephraseResultDTO> Rephrase(
        RephraseRequestModel model, string uId)
    {
        ISynonymDictionaryService dictionaryService = model.Language.ToLower() switch
        {
            "rus" => new RusSynonymDictionaryService(),
            "eng" => new EngSynonymDictionaryService(),
            _ => throw new Exception("language is null")
        };

        RephraseResult rephraseResult = _rephraseService.Rephrase(
            model.Text, dictionaryService);

        _ = UpdateRephraseHistory(rephraseResult, uId);
        
        return _mapper.Map<RephraseResultDTO>(
            rephraseResult
        );
    }
    
    private async Task UpdateRephraseHistory(RephraseResult rephraseResult, string uId)
    {
        User user = await _userRepository.GetByIdAsync(uId);

        user.RephraseHistory.Add(rephraseResult);

        await _userRepository.UpdateAsync(user);
        
        await _userRepository.SaveChangesAsync();
    }
}
