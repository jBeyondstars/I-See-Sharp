using ISeeSharp.Domain.Interfaces;
using MediatR;

namespace ISeeSharp.Application.Sessions.Queries.GetSessions;

public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, IEnumerable<SessionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSessionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SessionDto>> Handle(
        GetSessionsQuery request,
        CancellationToken cancellationToken)
    {
        var sessions = await _unitOfWork.Sessions.GetActiveSessionsAsync(cancellationToken);

        return sessions.Select(s => new SessionDto(
            s.Id,
            s.Title,
            s.Slug,
            s.Description,
            s.Difficulty.ToString(),
            s.Category.ToString(),
            s.Points,
            s.EstimatedMinutes,
            s.IsPremium,
            s.Files.Count,
            s.TotalAttempts,
            s.TotalCompletions));
    }
}
