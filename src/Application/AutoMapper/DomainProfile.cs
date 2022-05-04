using Application.PlagiarismCheck.DTOs;
using AutoMapper;
using Synword.Domain.Entities.PlagiarismCheckAggregate;

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
    }
}
