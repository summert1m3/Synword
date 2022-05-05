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

        StringBuilder rephrasedTextBuilder = new(text);

        List<Synonym> replacedWords = new();
        
        int offset = 0;
        
        foreach (var word in words)
        {
            bool isWordFounded = _dictionary.TryGetValue(
                word.Word, out IReadOnlyList
                    <DictionarySynonym>? synonyms);

            if (!isWordFounded)
            {
                continue;
            }

            word.StartIndex += offset;
            
            rephrasedTextBuilder.Remove(word.StartIndex, word.Word.Length);
            rephrasedTextBuilder.Insert(word.StartIndex, synonyms![0].Value);
            
            if (word.Word.Length < synonyms![0].Value.Length)
            {
                offset -= word.Word.Length - synonyms![0].Value.Length;
            }
            else if (word.Word.Length > synonyms![0].Value.Length)
            {
                offset += synonyms![0].Value.Length - word.Word.Length;
            }

            List<string> synonymsStr = new();
            foreach (var item in synonyms)
            {
                synonymsStr.Add(item.Value);
            }
            
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
}


internal class WordWithStartIndexModel
{
    public string Word { get; set; }
    public int StartIndex { get; set; }
}
