using MediatR;

namespace ISeeSharp.Application.Leaderboard.Queries.GetLeaderboard;

public record GetLeaderboardQuery(int Top = 10) : IRequest<IEnumerable<LeaderboardEntryDto>>;

public record LeaderboardEntryDto(
    int Rank,
    string Username,
    int TotalScore,
    int ExercisesCompleted);
