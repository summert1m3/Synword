using Application.Rephrase.DTOs;
using Application.Validation;
using Ardalis.GuardClauses;
using AutoMapper;
using Synword.Domain.Constants;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;
using Synword.Infrastructure.SynonymDictionary.EngSynonymDictionary.Queries;
using Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary.Queries;

namespace Application.Rephrase.Services;

public class AppRephraseService : IAppRephraseService
{
    private readonly IMapper _mapper;
    private readonly IRephraseService _rephraseService;
    private readonly ISynwordRepository<User> _userRepository;
    private readonly IUserValidation _userValidation;
    
    public AppRephraseService(
        IMapper mapper, 
        IRephraseService rephraseService,
        ISynwordRepository<User> userRepository,
        IUserValidation userValidation)
    {
        _mapper = mapper;
        _rephraseService = rephraseService;
        _userRepository = userRepository;
        _userValidation = userValidation;
    }
    
    public async Task<RephraseResultDTO> Rephrase(
        RephraseRequestModel model, string uId)
    {
        User? user = await _userRepository.GetByIdAsync(uId);
        Guard.Against.Null(user, nameof(user));
        
        bool isValid = _userValidation.IsValid(
            user, AppServicePricesConstants.RephrasePrice);

        if (!isValid)
        {
            throw new Exception(_userValidation.ErrorMessage);
        }
        
        ISynonymDictionaryService dictionaryService = model.Language.ToLower() switch
        {
            "rus" => new RusSynonymDictionaryService(),
            "eng" => new EngSynonymDictionaryService(),
            _ => throw new Exception("language is null")
        };

        RephraseResult rephraseResult = _rephraseService.Rephrase(
            model.Text, dictionaryService);

        await UpdateRephraseHistory(rephraseResult, user);
        
        return _mapper.Map<RephraseResultDTO>(
            rephraseResult
        );
    }
    
    private async Task UpdateRephraseHistory(
        RephraseResult rephraseResult, User user)
    {
        user.RecordRephraseResultInHistory(rephraseResult);

        await _userRepository.UpdateAsync(user);
        
        await _userRepository.SaveChangesAsync();
    }
}
