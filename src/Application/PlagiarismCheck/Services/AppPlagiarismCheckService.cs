using Application.PlagiarismCheck.DTOs;
using Application.Validation;
using AutoMapper;
using Synword.Domain.Constants;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;

namespace Application.PlagiarismCheck.Services;

public class AppPlagiarismCheckService : IAppPlagiarismCheckService
{
    private readonly IMapper _mapper;
    private readonly IPlagiarismCheckService _plagiarismCheck;
    private readonly ISynwordRepository<User>? _userRepository;
    private readonly IUserValidation _userValidation;
    
    public AppPlagiarismCheckService(
        IMapper mapper,
        IPlagiarismCheckService plagiarismCheck,
        ISynwordRepository<User> userRepository,
        IUserValidation userValidation)
    {
        _mapper = mapper;
        _plagiarismCheck = plagiarismCheck;
        _userRepository = userRepository;
        _userValidation = userValidation;
    }
    
    public async Task<PlagiarismCheckResultDTO> CheckPlagiarism(
        string text, string uId)
    {
        User user = await _userRepository.GetByIdAsync(uId);

        bool isValid = _userValidation.IsValid(
            user, AppServicePricesConstants.PlagiarismCheckPrice);

        if (!isValid)
        {
            throw new Exception(_userValidation.ErrorMessage);
        }

        PlagiarismCheckResult plagiarismCheckResult = 
            await _plagiarismCheck.CheckPlagiarism(text);

        _ = UpdatePlagiarismCheckHistory(plagiarismCheckResult, user);
        
        return _mapper.Map<PlagiarismCheckResultDTO>(
                plagiarismCheckResult
            );
    }

    private async Task UpdatePlagiarismCheckHistory(
        PlagiarismCheckResult plagiarismCheckResult, User user)
    {
        user.PlagiarismCheckHistory.Add(plagiarismCheckResult);

        await _userRepository.UpdateAsync(user);
        
        await _userRepository.SaveChangesAsync();
    }
}
