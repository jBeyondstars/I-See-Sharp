using ISeeSharp.Domain.Entities;
using ISeeSharp.Domain.Interfaces;
using ISeeSharp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ISeeSharp.Infrastructure.Repositories;

public class SessionRepository : Repository<Session>, ISessionRepository
{
    public SessionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Session?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(s => s.Slug == slug, cancellationToken);
    }

    public async Task<Session?> GetBySlugWithFilesAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(s => s.Files.OrderBy(f => f.SortOrder))
            .FirstOrDefaultAsync(s => s.Slug == slug, cancellationToken);
    }

    public async Task<IEnumerable<Session>> GetByCategoryAsync(SessionCategory category, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(s => s.Category == category && s.IsActive)
            .OrderBy(s => s.Difficulty)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Session>> GetByDifficultyAsync(SessionDifficulty difficulty, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(s => s.Difficulty == difficulty && s.IsActive)
            .OrderBy(s => s.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Session>> GetActiveSessionsAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(s => s.IsActive)
            .OrderBy(s => s.Difficulty)
            .ThenBy(s => s.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Session>> GetPremiumSessionsAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(s => s.IsPremium && s.IsActive)
            .OrderBy(s => s.Difficulty)
            .ThenBy(s => s.Category)
            .ToListAsync(cancellationToken);
    }
}
