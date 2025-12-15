using ISeeSharp.Domain.Entities;
using ISeeSharp.Domain.Interfaces;
using ISeeSharp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ISeeSharp.Infrastructure.Repositories;

public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
{
    public ExerciseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Exercise?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Slug == slug, cancellationToken);
    }

    public async Task<IEnumerable<Exercise>> GetByCategoryAsync(
        ExerciseCategory category,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(e => e.Category == category && e.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Exercise>> GetByDifficultyAsync(
        ExerciseDifficulty difficulty,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(e => e.Difficulty == difficulty && e.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Exercise>> GetActiveExercisesAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(e => e.IsActive)
            .OrderBy(e => e.Difficulty)
            .ThenBy(e => e.Category)
            .ToListAsync(cancellationToken);
    }
}
