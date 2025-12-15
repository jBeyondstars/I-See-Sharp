using ISeeSharp.Application.Exercises.Queries.GetExerciseBySlug;
using ISeeSharp.Application.Exercises.Queries.GetExercises;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ISeeSharp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly ISender _sender;

    public ExercisesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetExercisesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<ExerciseDetailDto>> GetBySlug(
        string slug,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetExerciseBySlugQuery(slug), cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
