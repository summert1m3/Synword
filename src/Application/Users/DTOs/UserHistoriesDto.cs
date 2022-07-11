using Application.PlagiarismCheck.DTOs;
using Application.Rephrase.DTOs;
using Application.Rephrase.DTOs.RephraseResult;

namespace Application.Users.DTOs;

public class UserHistoriesDto
{
    public List<RephraseResultDto> RephraseHistories { get; set; }
    public List<PlagiarismCheckResultDto> PlagiarismCheckHistories { get; set; }
}
