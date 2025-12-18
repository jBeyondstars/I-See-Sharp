using ISeeSharp.Domain.Entities;

namespace ISeeSharp.Domain.Interfaces;

public interface ISessionRepository : IRepository<Session>
{
    Task<Session?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<Session?> GetBySlugWithFilesAsync(string slug, CancellationToken cancellationToken = default);
    Task<IEnumerable<Session>> GetByCategoryAsync(SessionCategory category, CancellationToken cancellationToken = default);
    Task<IEnumerable<Session>> GetByDifficultyAsync(SessionDifficulty difficulty, CancellationToken cancellationToken = default);
    Task<IEnumerable<Session>> GetActiveSessionsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Session>> GetPremiumSessionsAsync(CancellationToken cancellationToken = default);
}
