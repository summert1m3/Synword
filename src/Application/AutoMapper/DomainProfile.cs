using Application.PlagiarismCheck.DTOs;
using AutoMapper;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Services.PlagiarismCheck;

namespace Application.AutoMapper;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<PlagiarismCheckResponseModel, 
            PlagiarismCheckResponseDTO>();
        CreateMap<HighlightRange, 
            HighlightRangeDTO>();
        CreateMap<MatchedUrl, MatchedUrlDTO>();
    }
}
