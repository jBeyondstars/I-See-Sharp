using ISeeSharp.Application.Leaderboard.Queries.GetLeaderboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ISeeSharp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly ISender _sender;

    public LeaderboardController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaderboardEntryDto>>> Get(
        [FromQuery] int top = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetLeaderboardQuery(top), cancellationToken);
        return Ok(result);
    }
}
