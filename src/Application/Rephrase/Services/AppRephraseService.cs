using Application.Rephrase.DTOs;
using AutoMapper;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.Rephrase;
using Synword.Infrastructure.SynonymDictionary.EngSynonymDictionary.Queries;
using Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary.Queries;

namespace Application.Rephrase;

public class AppRephraseService : IAppRephraseService
{
    private readonly IMapper _mapper;
    private readonly IRephraseService _rephraseService;
    public AppRephraseService(IMapper mapper, IRephraseService rephraseService)
    {
        _mapper = mapper;
        _rephraseService = rephraseService;
    }
    public RephraseResultDTO Rephrase(RephraseRequestModel model)
    {
        ISynonymDictionaryService dictionaryService = model.Language.ToLower() switch
        {
            "rus" => new RusSynonymDictionaryService(),
            "eng" => new EngSynonymDictionaryService(),
            _ => throw new Exception("language is null")
        };

        return _mapper.Map<RephraseResultDTO>(
            _rephraseService.Rephrase(model.Text, dictionaryService)
        );
    }
}
