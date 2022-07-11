using Application.AppFeatures.PlagiarismCheck.DTOs;
using Application.Exceptions;
using Application.Validation.PlagiarismCheckValidation;
using Ardalis.GuardClauses;
using AutoMapper;
using Synword.Domain.Constants;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;

namespace Application.AppFeatures.PlagiarismCheck.Services;

public class AppPlagiarismCheckService : IAppPlagiarismCheckService
{
    private readonly IMapper _mapper;
    private readonly IPlagiarismCheckService _plagiarismCheck;
    private readonly ISynwordRepository<User> _userRepository;
    private readonly IPlagiarismRequestValidation _validation;
    
    public AppPlagiarismCheckService(
        IMapper mapper,
        IPlagiarismCheckService plagiarismCheck,
        ISynwordRepository<User> userRepository,
        IPlagiarismRequestValidation validation)
    {
        _mapper = mapper;
        _plagiarismCheck = plagiarismCheck;
        _userRepository = userRepository;
        _validation = validation;
    }
    
    public async Task<PlagiarismCheckResultDto> CheckPlagiarism(
        string text, string uId)
    {
        User? user = await _userRepository.GetByIdAsync(uId);
        Guard.Against.Null(user, nameof(user));

        int price = CalculatePrice(text);
        
        bool isValid = _validation.IsValid(
            user, text, price);

        if (!isValid)
        {
            throw new AppValidationException(_validation.ErrorMessage);
        }

        PlagiarismCheckResult result = 
            await _plagiarismCheck.CheckPlagiarism(text);
        
        user.SpendCoins(price);
        
        user.RecordPlagiarismResultInHistory(result);

        await _userRepository.UpdateAsync(user);
        
        await _userRepository.SaveChangesAsync();
        
        return _mapper.Map<PlagiarismCheckResultDto>(
                result
            );
    }

    private int CalculatePrice(string text)
    {
        int count = 
            (int)Math.Ceiling(
                text.Length / (float)ExternalServicesConstants.ApiInputRestriction);
        int price = ServicePricesConstants.PlagiarismCheckPrice * count;

        return price;
    }
}
