using ISeeSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISeeSharp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Exercise> Exercises { get; }
    DbSet<UserExerciseResult> UserExerciseResults { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
