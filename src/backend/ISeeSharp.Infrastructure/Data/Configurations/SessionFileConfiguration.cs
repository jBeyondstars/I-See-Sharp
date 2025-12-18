using ISeeSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISeeSharp.Infrastructure.Data.Configurations;

public class SessionFileConfiguration : IEntityTypeConfiguration<SessionFile>
{
    public void Configure(EntityTypeBuilder<SessionFile> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Path)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(f => f.DisplayName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.Language)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(f => f.TargetContent)
            .IsRequired();

        builder.Property(f => f.EditableRegionsJson)
            .HasColumnType("jsonb");

        builder.HasIndex(f => f.SessionId);
        builder.HasIndex(f => new { f.SessionId, f.SortOrder });
    }
}
