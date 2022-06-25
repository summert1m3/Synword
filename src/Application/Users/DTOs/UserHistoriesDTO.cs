using Application.PlagiarismCheck.DTOs;
using Application.Rephrase.DTOs;

namespace Application.Users.DTOs;

public class UserHistoriesDTO
{
    public List<RephraseResultDTO> RephraseHistories { get; set; }
    public List<PlagiarismCheckResultDTO> PlagiarismCheckHistories { get; set; }
}
