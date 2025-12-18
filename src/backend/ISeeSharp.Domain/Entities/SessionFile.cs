namespace ISeeSharp.Domain.Entities;

public class SessionFile : Entity
{
    public Guid SessionId { get; private set; }
    public Session Session { get; private set; } = null!;

    public string Path { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public string Language { get; private set; } = "csharp";

    public string TargetContent { get; private set; } = string.Empty;
    public string? EditableRegionsJson { get; private set; }

    public int SortOrder { get; private set; }
    public int TotalLines { get; private set; }
    public int TotalCharacters { get; private set; }

    private SessionFile() { }

    public static SessionFile Create(
        Guid sessionId,
        string path,
        string displayName,
        string targetContent,
        int sortOrder,
        string language = "csharp",
        string? editableRegionsJson = null)
    {
        var lines = targetContent.Split('\n').Length;
        var chars = targetContent.Length;

        return new SessionFile
        {
            SessionId = sessionId,
            Path = path,
            DisplayName = displayName,
            Language = language,
            TargetContent = targetContent,
            EditableRegionsJson = editableRegionsJson,
            SortOrder = sortOrder,
            TotalLines = lines,
            TotalCharacters = chars
        };
    }

    public void UpdateContent(string targetContent, string? editableRegionsJson = null)
    {
        TargetContent = targetContent;
        EditableRegionsJson = editableRegionsJson;
        TotalLines = targetContent.Split('\n').Length;
        TotalCharacters = targetContent.Length;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateSortOrder(int sortOrder)
    {
        SortOrder = sortOrder;
        UpdatedAt = DateTime.UtcNow;
    }
}
