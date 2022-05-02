using Application.PlagiarismCheck.DTOs;
using AutoMapper;
using Synword.Domain.Interfaces;
using Synword.Domain.Interfaces.Services;

namespace Application.PlagiarismCheck.Services;

public class AppPlagiarismCheckService : IAppPlagiarismCheckService
{
    private readonly IMapper _mapper;
    private readonly IPlagiarismCheckService _plagiarismCheck;
    
    public AppPlagiarismCheckService(IMapper mapper,
        IPlagiarismCheckService plagiarismCheck)
    {
        _mapper = mapper;
        _plagiarismCheck = plagiarismCheck;
    }
    
    public async Task<PlagiarismCheckResponseDTO> CheckPlagiarism(string text)
    {
        return _mapper.Map<PlagiarismCheckResponseDTO>(
                await _plagiarismCheck.CheckPlagiarism(text)
            );
    }
}
