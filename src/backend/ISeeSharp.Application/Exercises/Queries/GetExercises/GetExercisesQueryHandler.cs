using ISeeSharp.Domain.Interfaces;
using MediatR;

namespace ISeeSharp.Application.Exercises.Queries.GetExercises;

public class GetExercisesQueryHandler : IRequestHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetExercisesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ExerciseDto>> Handle(
        GetExercisesQuery request,
        CancellationToken cancellationToken)
    {
        var exercises = await _unitOfWork.Exercises.GetActiveExercisesAsync(cancellationToken);

        return exercises.Select(e => new ExerciseDto(
            e.Id,
            e.Title,
            e.Slug,
            e.Description,
            e.Difficulty.ToString(),
            e.Category.ToString(),
            e.Points));
    }
}
