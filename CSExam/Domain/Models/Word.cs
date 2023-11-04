using CSExam.Domain.Repositories;

namespace CSExam.Domain.Models;

public record Word (int Id, string Value, string Language) : IHasId;
