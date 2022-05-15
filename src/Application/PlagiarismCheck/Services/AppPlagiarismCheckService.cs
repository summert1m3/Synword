using Application.PlagiarismCheck.DTOs;
using AutoMapper;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;

namespace Application.PlagiarismCheck.Services;

public class AppPlagiarismCheckService : IAppPlagiarismCheckService
{
    private readonly IMapper _mapper;
    private readonly IPlagiarismCheckService _plagiarismCheck;
    private readonly IUserDataRepository<User>? _userRepository;
    
    public AppPlagiarismCheckService(
        IMapper mapper,
        IPlagiarismCheckService plagiarismCheck,
        IUserDataRepository<User> userRepository)
    {
        _mapper = mapper;
        _plagiarismCheck = plagiarismCheck;
        _userRepository = userRepository;
    }
    
    public async Task<PlagiarismCheckResultDTO> CheckPlagiarism(
        string text, string uId)
    {
        PlagiarismCheckResult plagiarismCheckResult = 
            await _plagiarismCheck.CheckPlagiarism(text);

        UpdatePlagiarismCheckHistory(plagiarismCheckResult, uId);
        
        return _mapper.Map<PlagiarismCheckResultDTO>(
                plagiarismCheckResult
            );
    }

    private async Task UpdatePlagiarismCheckHistory(
        PlagiarismCheckResult plagiarismCheckResult, string uId)
    {
        User user = await _userRepository.GetByIdAsync(uId);

        user.PlagiarismCheckHistory.Add(plagiarismCheckResult);

        await _userRepository.UpdateAsync(user);
        
        await _userRepository.SaveChangesAsync();
    }
}
