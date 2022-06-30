using Application.Exceptions;
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
    private readonly IRephraseRequestValidation _validation;
    
    public AppRephraseService(
        IMapper mapper, 
        IRephraseService rephraseService,
        ISynwordRepository<User> userRepository,
        IRephraseRequestValidation validation)
    {
        _mapper = mapper;
        _rephraseService = rephraseService;
        _userRepository = userRepository;
        _validation = validation;
    }
    
    public async Task<RephraseResultDTO> Rephrase(
        RephraseRequestModel model, string uId)
    {
        User? user = await _userRepository.GetByIdAsync(uId);
        Guard.Against.Null(user, nameof(user));

        int price = CalculatePrice(model.Text);
        
        bool isValid = _validation.IsValid(
            user, model.Text, price);

        if (!isValid)
        {
            throw new AppValidationException(_validation.ErrorMessage);
        }
        
        ISynonymDictionaryService dictionaryService = model.Language.ToLower() switch
        {
            "rus" => new RusSynonymDictionaryService(),
            "eng" => new EngSynonymDictionaryService(),
            _ => throw new Exception("language is null")
        };

        RephraseResult rephraseResult = _rephraseService.Rephrase(
            model.Text, dictionaryService);
        
        user.SpendCoins(price);
        
        user.RecordRephraseResultInHistory(rephraseResult);

        await _userRepository.UpdateAsync(user);
        
        await _userRepository.SaveChangesAsync();
        
        return _mapper.Map<RephraseResultDTO>(
            rephraseResult
        );
    }

    private int CalculatePrice(string text)
    {
        int count = 
            (int)Math.Ceiling(text.Length / (float)ExternalServicesConstants.ApiInputRestriction);
        int price = ServicePricesConstants.RephrasePrice * count;

        return price;
    }
}
