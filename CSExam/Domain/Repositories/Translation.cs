using System.Text.Json;
using CSExam.Domain.Models;

namespace CSExam.Domain.Repositories;


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
