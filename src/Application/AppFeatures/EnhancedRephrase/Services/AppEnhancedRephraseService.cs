using Ardalis.GuardClauses;
using Synword.Application.AppFeatures.EnhancedRephrase.DTOs;
using Synword.Application.Validation.EnhancedRephraseValidation;
using Synword.Domain.Constants;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.EnhancedRephrase;

namespace Synword.Application.AppFeatures.EnhancedRephrase.Services;

public class AppEnhancedRephraseService : IAppEnhancedRephraseService
{
    private readonly IEnhancedRephraseService _service;
    private readonly ISynwordRepository<User> _userRepository;
    private readonly IEnhancedRephraseRequestValidation _validation;
    
    public AppEnhancedRephraseService(
        IEnhancedRephraseService service,
        ISynwordRepository<User> userRepository,
        IEnhancedRephraseRequestValidation validation)
    {
        _service = service;
        _userRepository = userRepository;
        _validation = validation;
    }
    
    public async Task<EnhancedRephraseResult> Rephrase(
        EnhancedRephraseRequestDto request,
        string uId)
    {
        User? user = await _userRepository.GetByIdAsync(uId);
        Guard.Against.Null(user, nameof(user));
        
        bool isValid = _validation.IsValid(
            user, 
            request.Text, 
            ServicePricesConstants.EnhancedRephrasePrice);

        if (!isValid)
        {
            throw new Exception(_validation.ErrorMessage);
        }

        EnhancedRephraseResult result = 
            await _service.Rephrase(request.Text);
        
        user.SpendCoins(ServicePricesConstants.EnhancedRephrasePrice);

        await _userRepository.UpdateAsync(user);
        
        await _userRepository.SaveChangesAsync();
        
        return result;
    }
}
