using ISeeSharp.Domain.Entities;
using ISeeSharp.Domain.Interfaces;
using ISeeSharp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ISeeSharp.Infrastructure.Repositories;

public class UserSessionResultRepository : Repository<UserSessionResult>, IUserSessionResultRepository
{
    public UserSessionResultRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<UserSessionResult>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(r => r.UserId == userId)
            .Include(r => r.Session)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserSessionResult>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(r => r.SessionId == sessionId)
            .Include(r => r.User)
            .OrderByDescending(r => r.ScoreEarned)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserSessionResult?> GetBestResultAsync(Guid userId, Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(r => r.UserId == userId && r.SessionId == sessionId && r.IsCompleted)
            .OrderByDescending(r => r.ScoreEarned)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> GetAttemptCountAsync(Guid userId, Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await DbSet.CountAsync(r => r.UserId == userId && r.SessionId == sessionId, cancellationToken);
    }

    public async Task<IEnumerable<UserSessionResult>> GetRecentByUserIdAsync(Guid userId, int count, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .Take(count)
            .Include(r => r.Session)
            .ToListAsync(cancellationToken);
    }
}
