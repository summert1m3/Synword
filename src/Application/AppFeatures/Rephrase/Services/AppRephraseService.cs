using Ardalis.GuardClauses;
using AutoMapper;
using Synword.Application.AppFeatures.Rephrase.DTOs;
using Synword.Application.AppFeatures.Rephrase.DTOs.RephraseResult;
using Synword.Application.Exceptions;
using Synword.Application.Validation.RephraseValidation;
using Synword.Domain.Constants;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;

namespace Synword.Application.AppFeatures.Rephrase.Services;

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
    
    public async Task<RephraseResultDto> Rephrase(
        RephraseRequestDto model, 
        ISynonymDictionaryService dictionaryService, 
        string uId)
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

        RephraseResult rephraseResult = _rephraseService.Rephrase(
            model.Text, dictionaryService);
        
        user.SpendCoins(price);
        
        user.RecordRephraseResultInHistory(rephraseResult);

        await _userRepository.UpdateAsync(user);
        
        await _userRepository.SaveChangesAsync();
        
        return _mapper.Map<RephraseResultDto>(
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
