using ISeeSharp.Domain.Entities;

namespace ISeeSharp.Domain.Interfaces;

public interface ISessionFileRepository : IRepository<SessionFile>
{
    Task<IEnumerable<SessionFile>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default);
    Task<SessionFile?> GetBySessionIdAndOrderAsync(Guid sessionId, int sortOrder, CancellationToken cancellationToken = default);
}
