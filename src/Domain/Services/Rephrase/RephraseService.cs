using System.Text;
using System.Text.RegularExpressions;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Entities.SynonymDictionaryAggregate;
using Synword.Domain.Interfaces.Services;

namespace Synword.Domain.Services.Rephrase;

public class RephraseService : IRephraseService
{
    private IReadOnlyDictionary<string, 
        IReadOnlyList<DictionarySynonym>> _dictionary;
    
    public RephraseResult Rephrase(
        string text, ISynonymDictionaryService dictionary)
    {
        _dictionary = dictionary.GetDictionary;

        List<WordWithStartIndexModel> words = GetWordsFromText(text);

        StringBuilder rephrasedTextBuilder = new(text);

        List<Synonym> replacedWords = new();
        
        int indexOffset = 0;
        
        foreach (var word in words)
        {
            bool isWordFounded = _dictionary.TryGetValue(
                word.Word, out IReadOnlyList
                    <DictionarySynonym>? synonyms);

            if (!isWordFounded)
            {
                continue;
            }

            word.StartIndex += indexOffset;

            rephrasedTextBuilder = ReplaceWordWithSynonym(
                rephrasedTextBuilder, word, synonyms![0].Value);
            
            indexOffset = IndexOffsetCorrection(
                indexOffset, 
                word.Word.Length,
                synonyms![0].Value.Length);

            List<string> synonymsStr = ConvertFromDictionarySynonymToString(synonyms);

            replacedWords.Add(
                    new (
                            word.Word,
                            word.StartIndex, 
                            word.StartIndex + synonymsStr[0].Length - 1,
                            synonymsStr
                        )
                );
        }

        RephraseResult rephraseResult = new(
                text,
                rephrasedTextBuilder.ToString(),
                replacedWords
            );

        return rephraseResult;
    }

    private List<WordWithStartIndexModel> GetWordsFromText(string text)
    {
        MatchCollection col = Regex.Matches(text,
            @"((?<part>[^\.\-\ \,]+)(\.\-\ \,))*(?<part>[^\.\-\ \,]+)");

        List<WordWithStartIndexModel> words = new();
            
        foreach (Match match in col) {
            var part = match.Groups["part"];
            
            words.Add(
                new WordWithStartIndexModel()
                {
                    Word = part.Value,
                    StartIndex = part.Index
                }
            );
        }

        return words;
    }

    private List<string> ConvertFromDictionarySynonymToString(
        IReadOnlyList<DictionarySynonym> synonyms)
    {
        List<string> synonymsStr = new();
        foreach (var item in synonyms)
        {
            synonymsStr.Add(item.Value);
        }

        return synonymsStr;
    }

    private int IndexOffsetCorrection(
        int currentOffset, 
        int sourceWordLength, 
        int synonymWordLength)
    {
        if (sourceWordLength < synonymWordLength)
        {
            currentOffset -= sourceWordLength - synonymWordLength;
        }
        else if (sourceWordLength > synonymWordLength)
        {
            currentOffset += synonymWordLength - sourceWordLength;
        }

        return currentOffset;
    }

    private StringBuilder ReplaceWordWithSynonym(
        StringBuilder text, 
        WordWithStartIndexModel word,
        string synonym)
    {
        text.Remove(word.StartIndex, word.Word.Length);
        text.Insert(word.StartIndex, synonym);

        return text;
    }
}

internal class WordWithStartIndexModel
{
    public string Word { get; set; }
    public int StartIndex { get; set; }
}
