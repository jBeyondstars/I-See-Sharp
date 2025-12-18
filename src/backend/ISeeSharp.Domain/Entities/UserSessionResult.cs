namespace ISeeSharp.Domain.Entities;

public class UserSessionResult : Entity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Guid SessionId { get; private set; }
    public Session Session { get; private set; } = null!;

    public bool IsCompleted { get; private set; }
    public double Accuracy { get; private set; }
    public int Wpm { get; private set; }
    public int Cpm { get; private set; }
    public int ErrorCount { get; private set; }
    public TimeSpan CompletionTime { get; private set; }

    public int LinesTyped { get; private set; }
    public int CharactersTyped { get; private set; }
    public int FilesCompleted { get; private set; }
    public int TotalFiles { get; private set; }

    public int ScoreEarned { get; private set; }
    public int XpEarned { get; private set; }
    public int AttemptNumber { get; private set; }

    public string? FileResultsJson { get; private set; }
    public string? ErrorPositionsJson { get; private set; }

    private UserSessionResult() { }

    public static UserSessionResult Create(
        Guid userId,
        Guid sessionId,
        double accuracy,
        int wpm,
        int linesTyped,
        int charactersTyped,
        int filesCompleted,
        int totalFiles,
        TimeSpan completionTime,
        int errorCount,
        int scoreEarned,
        int xpEarned,
        int attemptNumber,
        int cpm = 0,
        string? fileResultsJson = null,
        string? errorPositionsJson = null)
    {
        return new UserSessionResult
        {
            UserId = userId,
            SessionId = sessionId,
            IsCompleted = filesCompleted == totalFiles,
            Accuracy = accuracy,
            Wpm = wpm,
            Cpm = cpm > 0 ? cpm : wpm * 5,
            ErrorCount = errorCount,
            CompletionTime = completionTime,
            LinesTyped = linesTyped,
            CharactersTyped = charactersTyped,
            FilesCompleted = filesCompleted,
            TotalFiles = totalFiles,
            ScoreEarned = scoreEarned,
            XpEarned = xpEarned,
            AttemptNumber = attemptNumber,
            FileResultsJson = fileResultsJson,
            ErrorPositionsJson = errorPositionsJson
        };
    }
}
