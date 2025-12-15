using ISeeSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISeeSharp.Infrastructure.Data.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(e => e.Instructions)
            .IsRequired();

        builder.Property(e => e.StarterCode)
            .IsRequired();

        builder.Property(e => e.Solution)
            .IsRequired();

        builder.Property(e => e.Difficulty)
            .HasConversion<string>();

        builder.Property(e => e.Category)
            .HasConversion<string>();

        builder.HasIndex(e => e.Slug)
            .IsUnique();

        builder.HasIndex(e => e.Category);
        builder.HasIndex(e => e.Difficulty);
    }
}
