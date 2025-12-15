using ISeeSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISeeSharp.Infrastructure.Data.Configurations;

public class UserExerciseResultConfiguration : IEntityTypeConfiguration<UserExerciseResult>
{
    public void Configure(EntityTypeBuilder<UserExerciseResult> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.SubmittedCode)
            .IsRequired();

        builder.HasOne(r => r.User)
            .WithMany(u => u.ExerciseResults)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Exercise)
            .WithMany(e => e.UserResults)
            .HasForeignKey(r => r.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => new { r.UserId, r.ExerciseId });
    }
}
