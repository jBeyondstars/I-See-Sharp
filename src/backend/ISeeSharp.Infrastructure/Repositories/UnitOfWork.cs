using ISeeSharp.Domain.Interfaces;
using ISeeSharp.Infrastructure.Data;

namespace ISeeSharp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IUserRepository? _users;
    private ISessionRepository? _sessions;
    private ISessionFileRepository? _sessionFiles;
    private IUserSessionResultRepository? _userSessionResults;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);
    public ISessionRepository Sessions => _sessions ??= new SessionRepository(_context);
    public ISessionFileRepository SessionFiles => _sessionFiles ??= new SessionFileRepository(_context);
    public IUserSessionResultRepository UserSessionResults =>
        _userSessionResults ??= new UserSessionResultRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
