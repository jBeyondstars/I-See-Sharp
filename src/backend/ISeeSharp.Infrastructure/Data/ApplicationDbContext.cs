using ISeeSharp.Application.Common.Interfaces;
using ISeeSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISeeSharp.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<SessionFile> SessionFiles => Set<SessionFile>();
    public DbSet<UserSessionResult> UserSessionResults => Set<UserSessionResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
