using ISeeSharp.Domain.Entities;
using ISeeSharp.Domain.Interfaces;
using ISeeSharp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ISeeSharp.Infrastructure.Repositories;

public class SessionFileRepository : Repository<SessionFile>, ISessionFileRepository
{
    public SessionFileRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<SessionFile>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(f => f.SessionId == sessionId)
            .OrderBy(f => f.SortOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<SessionFile?> GetBySessionIdAndOrderAsync(Guid sessionId, int sortOrder, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(f => f.SessionId == sessionId && f.SortOrder == sortOrder, cancellationToken);
    }
}
