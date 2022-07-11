using Application.AppFeatures.PlagiarismCheck.DTOs;
using Application.AppFeatures.Rephrase.DTOs.RephraseResult;
using AutoMapper;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Entities.RephraseAggregate;

namespace Application.AutoMapper;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<PlagiarismCheckResult, 
            PlagiarismCheckResultDto>()
            .ForMember(dest => dest.Id,
                opt 
                    => opt.MapFrom(src => src.History.Id));
        CreateMap<HighlightRange, 
            HighlightRangeDto>();
        CreateMap<MatchedUrl, MatchedUrlDto>();
        
        CreateMap<RephraseResult, RephraseResultDto>()
            .ForMember(dest => dest.Id,
                opt 
                    => opt.MapFrom(src => src.History.Id));
        CreateMap<SourceWordSynonyms, SourceWordSynonymsDto>();
        CreateMap<Synonym, SynonymDto>();
    }
}
