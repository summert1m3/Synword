using Application.PlagiarismCheck.DTOs;
using Application.Rephrase.DTOs;
using AutoMapper;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Entities.RephraseAggregate;

namespace Application.AutoMapper;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<PlagiarismCheckResult, 
            PlagiarismCheckResponseDTO>();
        CreateMap<HighlightRange, 
            HighlightRangeDTO>();
        CreateMap<MatchedUrl, MatchedUrlDTO>();
        
        CreateMap<RephraseResult, RephraseResultDTO>();
        CreateMap<Synonym, SynonymDTO>();
    }
}
