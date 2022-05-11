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
            PlagiarismCheckResultDTO>();
        CreateMap<HighlightRange, 
            HighlightRangeDTO>();
        CreateMap<MatchedUrl, MatchedUrlDTO>();
        
        CreateMap<RephraseResult, RephraseResultDTO>();
        CreateMap<SourceWordSynonyms, SourceWordSynonymsDTO>();
        CreateMap<Synonym, SynonymDTO>();
    }
}
