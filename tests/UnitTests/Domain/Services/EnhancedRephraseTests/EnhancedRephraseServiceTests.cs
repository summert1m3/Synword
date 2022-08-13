using System.Threading.Tasks;
using Moq;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.EnhancedRephrase;
using Xunit;

namespace UnitTests.Domain.Services.EnhancedRephraseTests;

public class EnhancedRephraseServiceTests
{
    private readonly Mock<IYandexTranslateApi> _rephraseApi = new();
    [Fact]
    public async Task EnhancedRephrase_ValidInput_ValidResult()
    {
        //Arrange
        string inputText =
            "String Tests define procedures to check specific feature at a high level.";

        _rephraseApi.Setup(
                x => x.Translate(
                    It.IsAny<string>(), It.IsAny<string>()))
            .Returns<string, string>(
                (targetLangCode, text) =>
                {
                    if (targetLangCode == "de")
                    {
                        return Task.Run(() =>
                        {
                            return new TranslatedTextModel()
                            {
                                DetectedLanguageCode = "en",
                                Text = "String-Tests definieren Prozeduren, um bestimmte Funktionen auf hoher Ebene zu überprüfen."
                            };
                        });
                    }
                    return Task.Run(() =>
                    {
                        return new TranslatedTextModel()
                        {
                            DetectedLanguageCode = "de",
                            Text = "String tests define procedures to check certain functions at a high level."
                        };
                    });
                });

        string expected =
            "String tests define procedures to check certain functions at a high level.";
        
        EnhancedRephraseService rephrase = new(_rephraseApi.Object);
        
        //Act
        var actual = await rephrase.Rephrase(inputText);
        
        //Assert
        Assert.Equal(expected, actual.RephrasedText);
    }
}
