using MediatR;

namespace ISeeSharp.Application.Sessions.Queries.GetSessionBySlug;

public record GetSessionBySlugQuery(string Slug) : IRequest<SessionDetailDto?>;

public record SessionDetailDto(
    Guid Id,
    string Title,
    string Slug,
    string Description,
    string Instructions,
    string Difficulty,
    string Category,
    int Points,
    int EstimatedMinutes,
    int TargetWpm,
    bool IsPremium,
    IEnumerable<SessionFileDto> Files);

public record SessionFileDto(
    Guid Id,
    string Path,
    string DisplayName,
    string Language,
    string TargetContent,
    string? EditableRegionsJson,
    int SortOrder,
    int TotalLines,
    int TotalCharacters);
