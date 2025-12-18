using ISeeSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISeeSharp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Session> Sessions { get; }
    DbSet<SessionFile> SessionFiles { get; }
    DbSet<UserSessionResult> UserSessionResults { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
