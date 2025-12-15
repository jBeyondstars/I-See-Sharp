using MediatR;

namespace ISeeSharp.Application.Exercises.Queries.GetExerciseBySlug;

public record GetExerciseBySlugQuery(string Slug) : IRequest<ExerciseDetailDto?>;

public record ExerciseDetailDto(
    Guid Id,
    string Title,
    string Slug,
    string Description,
    string Instructions,
    string StarterCode,
    string Difficulty,
    string Category,
    int Points);
