using ISeeSharp.Domain.Interfaces;
using MediatR;

namespace ISeeSharp.Application.Leaderboard.Queries.GetLeaderboard;

public class GetLeaderboardQueryHandler : IRequestHandler<GetLeaderboardQuery, IEnumerable<LeaderboardEntryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLeaderboardQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<LeaderboardEntryDto>> Handle(
        GetLeaderboardQuery request,
        CancellationToken cancellationToken)
    {
        var topUsers = await _unitOfWork.Users.GetTopUsersAsync(request.Top, cancellationToken);

        return topUsers.Select((user, index) => new LeaderboardEntryDto(
            index + 1,
            user.Username,
            user.TotalScore,
            user.SessionsCompleted));
    }
}
