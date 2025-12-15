using ISeeSharp.Domain.Entities;

namespace ISeeSharp.Domain.Interfaces;

public interface IUserExerciseResultRepository : IRepository<UserExerciseResult>
{
    Task<IEnumerable<UserExerciseResult>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserExerciseResult>> GetByExerciseIdAsync(Guid exerciseId, CancellationToken cancellationToken = default);
    Task<UserExerciseResult?> GetBestResultAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    Task<int> GetAttemptCountAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
}
