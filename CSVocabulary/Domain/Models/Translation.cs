using CSVocabulary.Domain.Repositories;

namespace CSVocabulary.Domain.Models;

public record Translation(int Id, List<int> WordIds) : IHasId;
