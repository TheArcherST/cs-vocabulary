using CSExam.Domain.Repositories;

namespace CSExam.Domain.Models;

public record Translation(int Id, List<int> WordIds) : IHasId;
