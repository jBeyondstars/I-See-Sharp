using ISeeSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISeeSharp.Infrastructure.Data.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(s => s.Instructions)
            .IsRequired();

        builder.Property(s => s.ObjectivesJson)
            .HasColumnType("jsonb");

        builder.Property(s => s.HintsJson)
            .HasColumnType("jsonb");

        builder.Property(s => s.Difficulty)
            .HasConversion<string>();

        builder.Property(s => s.Category)
            .HasConversion<string>();

        builder.HasMany(s => s.Files)
            .WithOne(f => f.Session)
            .HasForeignKey(f => f.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.UserResults)
            .WithOne(r => r.Session)
            .HasForeignKey(r => r.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.Slug).IsUnique();
        builder.HasIndex(s => s.Category);
        builder.HasIndex(s => s.Difficulty);
        builder.HasIndex(s => s.IsActive);
        builder.HasIndex(s => s.IsPremium);

        builder.Ignore(s => s.TotalLines);
        builder.Ignore(s => s.TotalCharacters);
    }
}
