namespace ISeeSharp.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ISessionRepository Sessions { get; }
    ISessionFileRepository SessionFiles { get; }
    IUserSessionResultRepository UserSessionResults { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
