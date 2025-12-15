namespace ISeeSharp.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IExerciseRepository Exercises { get; }
    IUserExerciseResultRepository UserExerciseResults { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
