using ISeeSharp.Domain.Interfaces;
using MediatR;

namespace ISeeSharp.Application.Exercises.Queries.GetExerciseBySlug;

public class GetExerciseBySlugQueryHandler : IRequestHandler<GetExerciseBySlugQuery, ExerciseDetailDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetExerciseBySlugQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ExerciseDetailDto?> Handle(
        GetExerciseBySlugQuery request,
        CancellationToken cancellationToken)
    {
        var exercise = await _unitOfWork.Exercises.GetBySlugAsync(request.Slug, cancellationToken);

        if (exercise is null)
        {
            return null;
        }

        return new ExerciseDetailDto(
            exercise.Id,
            exercise.Title,
            exercise.Slug,
            exercise.Description,
            exercise.Instructions,
            exercise.StarterCode,
            exercise.Difficulty.ToString(),
            exercise.Category.ToString(),
            exercise.Points);
    }
}
