using Application.PlagiarismCheck.DTOs;
using Application.Rephrase.DTOs;

namespace Application.Users.DTOs;

public class UserHistoriesDTO
{
    public List<RephraseResultDTO> rephraseHistories { get; set; }
    public List<PlagiarismCheckResultDTO> plagiarismCheckHistories { get; set; }
}
