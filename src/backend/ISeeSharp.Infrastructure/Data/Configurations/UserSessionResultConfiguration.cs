using ISeeSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISeeSharp.Infrastructure.Data.Configurations;

public class UserSessionResultConfiguration : IEntityTypeConfiguration<UserSessionResult>
{
    public void Configure(EntityTypeBuilder<UserSessionResult> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.User)
            .WithMany(u => u.SessionResults)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Session)
            .WithMany(s => s.UserResults)
            .HasForeignKey(r => r.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.FileResultsJson)
            .HasColumnType("jsonb");

        builder.Property(r => r.ErrorPositionsJson)
            .HasColumnType("jsonb");

        builder.HasIndex(r => new { r.UserId, r.SessionId });
        builder.HasIndex(r => new { r.UserId, r.CreatedAt });
        builder.HasIndex(r => r.SessionId);
    }
}
