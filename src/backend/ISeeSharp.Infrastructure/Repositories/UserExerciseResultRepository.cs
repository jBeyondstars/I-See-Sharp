using ISeeSharp.Domain.Entities;
using ISeeSharp.Domain.Interfaces;
using ISeeSharp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ISeeSharp.Infrastructure.Repositories;

public class UserExerciseResultRepository : Repository<UserExerciseResult>, IUserExerciseResultRepository
{
    public UserExerciseResultRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserExerciseResult>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(r => r.UserId == userId)
            .Include(r => r.Exercise)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserExerciseResult>> GetByExerciseIdAsync(
        Guid exerciseId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(r => r.ExerciseId == exerciseId)
            .Include(r => r.User)
            .OrderByDescending(r => r.ScoreEarned)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserExerciseResult?> GetBestResultAsync(
        Guid userId,
        Guid exerciseId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(r => r.UserId == userId && r.ExerciseId == exerciseId && r.IsCorrect)
            .OrderByDescending(r => r.ScoreEarned)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> GetAttemptCountAsync(
        Guid userId,
        Guid exerciseId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .CountAsync(r => r.UserId == userId && r.ExerciseId == exerciseId, cancellationToken);
    }
}
