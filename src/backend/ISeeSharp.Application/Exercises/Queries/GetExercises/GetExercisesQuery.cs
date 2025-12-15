using MediatR;

namespace ISeeSharp.Application.Exercises.Queries.GetExercises;

public record GetExercisesQuery : IRequest<IEnumerable<ExerciseDto>>;

public record ExerciseDto(
    Guid Id,
    string Title,
    string Slug,
    string Description,
    string Difficulty,
    string Category,
    int Points);
