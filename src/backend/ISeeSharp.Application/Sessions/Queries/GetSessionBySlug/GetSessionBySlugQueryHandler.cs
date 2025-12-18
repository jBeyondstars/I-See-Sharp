using ISeeSharp.Domain.Interfaces;
using MediatR;

namespace ISeeSharp.Application.Sessions.Queries.GetSessionBySlug;

public class GetSessionBySlugQueryHandler : IRequestHandler<GetSessionBySlugQuery, SessionDetailDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSessionBySlugQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SessionDetailDto?> Handle(
        GetSessionBySlugQuery request,
        CancellationToken cancellationToken)
    {
        var session = await _unitOfWork.Sessions.GetBySlugWithFilesAsync(request.Slug, cancellationToken);

        if (session is null)
        {
            return null;
        }

        return new SessionDetailDto(
            session.Id,
            session.Title,
            session.Slug,
            session.Description,
            session.Instructions,
            session.Difficulty.ToString(),
            session.Category.ToString(),
            session.Points,
            session.EstimatedMinutes,
            session.TargetWpm,
            session.IsPremium,
            session.Files.Select(f => new SessionFileDto(
                f.Id,
                f.Path,
                f.DisplayName,
                f.Language,
                f.TargetContent,
                f.EditableRegionsJson,
                f.SortOrder,
                f.TotalLines,
                f.TotalCharacters)));
    }
}
