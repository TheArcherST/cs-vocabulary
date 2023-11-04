using CSVocabulary.Domain.Models;

namespace CSVocabulary.Domain.Repositories;


public class WordRepo : BaseRepo<Word>
{
    public WordRepo(string filename) : base(filename) {}

    public Word Create(string value, string language)
    {
        return new Word(GetNextId(), value, language);
    }

    public Word Merge(string value, string language)
    {
        var result = GetByValue(value, language);
        if (result != null) return result;
        
        result = Create(value, language);
        Add(result);

        return result;
    }
    
    public Word? GetByValue(string value, string lang)
    {
        return Data.FirstOrDefault(word => word.Value == value && word.Language == lang);
    }
}
