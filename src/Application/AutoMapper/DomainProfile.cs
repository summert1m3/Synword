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
            PlagiarismCheckResultDTO>()
            .ForMember(dest => dest.Id,
                opt 
                    => opt.MapFrom(src => src.History.Id));
        CreateMap<HighlightRange, 
            HighlightRangeDTO>();
        CreateMap<MatchedUrl, MatchedUrlDTO>();
        
        CreateMap<RephraseResult, RephraseResultDTO>()
            .ForMember(dest => dest.Id,
                opt 
                    => opt.MapFrom(src => src.History.Id));
        CreateMap<SourceWordSynonyms, SourceWordSynonymsDTO>();
        CreateMap<Synonym, SynonymDTO>();
    }
}
