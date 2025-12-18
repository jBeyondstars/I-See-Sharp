namespace ISeeSharp.Domain.Entities;

public class Session : Entity
{
    public string Title { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Instructions { get; private set; } = string.Empty;
    public string? ObjectivesJson { get; private set; }
    public string? HintsJson { get; private set; }

    public SessionDifficulty Difficulty { get; private set; }
    public SessionCategory Category { get; private set; }

    public int Points { get; private set; }
    public int EstimatedMinutes { get; private set; }
    public int TargetWpm { get; private set; }

    public bool IsActive { get; private set; } = true;
    public bool IsPremium { get; private set; }

    public int TotalAttempts { get; private set; }
    public int TotalCompletions { get; private set; }
    public double AverageAccuracy { get; private set; }
    public double AverageWpm { get; private set; }

    private readonly List<SessionFile> _files = [];
    public IReadOnlyCollection<SessionFile> Files => _files.AsReadOnly();

    private readonly List<UserSessionResult> _userResults = [];
    public IReadOnlyCollection<UserSessionResult> UserResults => _userResults.AsReadOnly();

    public int TotalLines => _files.Sum(f => f.TotalLines);
    public int TotalCharacters => _files.Sum(f => f.TotalCharacters);

    private Session() { }

    public static Session Create(
        string title,
        string slug,
        string description,
        string instructions,
        SessionDifficulty difficulty,
        SessionCategory category,
        int points,
        int estimatedMinutes = 10,
        int targetWpm = 40,
        bool isPremium = false,
        string? objectivesJson = null,
        string? hintsJson = null)
    {
        return new Session
        {
            Title = title,
            Slug = slug,
            Description = description,
            Instructions = instructions,
            Difficulty = difficulty,
            Category = category,
            Points = points,
            EstimatedMinutes = estimatedMinutes,
            TargetWpm = targetWpm,
            IsPremium = isPremium,
            ObjectivesJson = objectivesJson,
            HintsJson = hintsJson
        };
    }

    public void AddFile(SessionFile file)
    {
        _files.Add(file);
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementAttempt()
    {
        TotalAttempts++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordCompletion(double accuracy, int wpm)
    {
        var totalCompleted = TotalCompletions + 1;
        AverageAccuracy = ((AverageAccuracy * TotalCompletions) + accuracy) / totalCompleted;
        AverageWpm = ((AverageWpm * TotalCompletions) + wpm) / totalCompleted;
        TotalCompletions = totalCompleted;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum SessionDifficulty
{
    Easy = 0,
    Medium = 1,
    Hard = 2,
    Expert = 3
}

public enum SessionCategory
{
    Syntax = 0,
    Variables = 1,
    ControlFlow = 2,
    Methods = 3,
    Classes = 4,
    Interfaces = 5,
    Inheritance = 6,
    Generics = 7,
    Collections = 8,
    Linq = 9,
    AsyncAwait = 10,
    ErrorHandling = 11,
    Patterns = 12,
    ModernCSharp = 13,
    FullProject = 14
}
