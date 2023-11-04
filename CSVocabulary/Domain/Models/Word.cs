using CSVocabulary.Domain.Repositories;

namespace CSVocabulary.Domain.Models;

public record Word (int Id, string Value, string Language) : IHasId;
