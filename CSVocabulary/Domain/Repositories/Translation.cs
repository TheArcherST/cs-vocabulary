using CSVocabulary.Domain.Models;

namespace CSVocabulary.Domain.Repositories;


public class TranslationRepo : BaseRepo<Translation>
{
    public TranslationRepo(string filename) : base(filename) {}

    public Translation Create(List<int> wordIds)
    {
        return new Translation(GetNextId(), wordIds);
    }
    
    public Translation? GetByWordId(int originalWordId)
    {
        return Data
            .FirstOrDefault(translation => translation.WordIds.Contains(originalWordId));
    }
}
