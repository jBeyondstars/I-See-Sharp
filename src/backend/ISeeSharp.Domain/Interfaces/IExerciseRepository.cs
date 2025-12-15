using ISeeSharp.Domain.Entities;

namespace ISeeSharp.Domain.Interfaces;

public interface IExerciseRepository : IRepository<Exercise>
{
    Task<Exercise?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IEnumerable<Exercise>> GetByCategoryAsync(ExerciseCategory category, CancellationToken cancellationToken = default);
    Task<IEnumerable<Exercise>> GetByDifficultyAsync(ExerciseDifficulty difficulty, CancellationToken cancellationToken = default);
    Task<IEnumerable<Exercise>> GetActiveExercisesAsync(CancellationToken cancellationToken = default);
}
