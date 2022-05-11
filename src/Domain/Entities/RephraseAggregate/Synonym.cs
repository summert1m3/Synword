namespace Synword.Domain.Entities.RephraseAggregate;

public class Synonym : BaseEntity
{
    private Synonym()
    {
        // required by EF
    }

    public Synonym(string synonym)
    {
        Value = synonym;
    }
    
    public string Value { get; private set; }
}
