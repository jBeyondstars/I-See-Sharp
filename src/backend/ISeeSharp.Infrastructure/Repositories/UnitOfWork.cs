using ISeeSharp.Domain.Interfaces;
using ISeeSharp.Infrastructure.Data;

namespace ISeeSharp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IUserRepository? _users;
    private IExerciseRepository? _exercises;
    private IUserExerciseResultRepository? _userExerciseResults;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IExerciseRepository Exercises => _exercises ??= new ExerciseRepository(_context);
    public IUserExerciseResultRepository UserExerciseResults =>
        _userExerciseResults ??= new UserExerciseResultRepository(_context);

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
