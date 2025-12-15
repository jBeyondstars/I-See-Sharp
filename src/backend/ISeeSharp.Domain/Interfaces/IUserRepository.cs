using ISeeSharp.Domain.Entities;

namespace ISeeSharp.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetTopUsersAsync(int count, CancellationToken cancellationToken = default);
}
