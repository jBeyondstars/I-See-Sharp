using MediatR;

namespace ISeeSharp.Application.Sessions.Queries.GetSessions;

public record GetSessionsQuery : IRequest<IEnumerable<SessionDto>>;

public record SessionDto(
    Guid Id,
    string Title,
    string Slug,
    string Description,
    string Difficulty,
    string Category,
    int Points,
    int EstimatedMinutes,
    bool IsPremium,
    int FileCount,
    int TotalAttempts,
    int TotalCompletions);
