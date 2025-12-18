using ISeeSharp.Domain.Entities;

namespace ISeeSharp.Domain.Interfaces;

public interface IUserSessionResultRepository : IRepository<UserSessionResult>
{
    Task<IEnumerable<UserSessionResult>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserSessionResult>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default);
    Task<UserSessionResult?> GetBestResultAsync(Guid userId, Guid sessionId, CancellationToken cancellationToken = default);
    Task<int> GetAttemptCountAsync(Guid userId, Guid sessionId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserSessionResult>> GetRecentByUserIdAsync(Guid userId, int count, CancellationToken cancellationToken = default);
}
