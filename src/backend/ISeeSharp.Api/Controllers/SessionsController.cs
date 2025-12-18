using ISeeSharp.Application.Sessions.Queries.GetSessionBySlug;
using ISeeSharp.Application.Sessions.Queries.GetSessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ISeeSharp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly ISender _sender;

    public SessionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SessionDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetSessionsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<SessionDetailDto>> GetBySlug(
        string slug,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetSessionBySlugQuery(slug), cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
