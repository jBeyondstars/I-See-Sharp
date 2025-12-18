namespace ISeeSharp.Domain.Entities;

public class User : Entity
{
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;

    public int TotalScore { get; private set; }
    public int TotalXp { get; private set; }
    public int Level { get; private set; } = 1;
    public int SessionsCompleted { get; private set; }

    public long TotalLinesWritten { get; private set; }
    public long TotalCharactersTyped { get; private set; }
    public long TotalTimeSeconds { get; private set; }
    public double AverageWpm { get; private set; }
    public double AverageAccuracy { get; private set; }

    public int CurrentStreak { get; private set; }
    public int LongestStreak { get; private set; }
    public DateTime? LastActivityDate { get; private set; }
    public int FreezeTokens { get; private set; }

    public string? PreferencesJson { get; private set; }

    private readonly List<UserSessionResult> _sessionResults = [];
    public IReadOnlyCollection<UserSessionResult> SessionResults => _sessionResults.AsReadOnly();

    private User() { }

    public static User Create(string username, string email, string passwordHash)
    {
        return new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash
        };
    }

    public void AddScore(int points)
    {
        TotalScore += points;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddXp(int xp)
    {
        TotalXp += xp;
        UpdateLevel();
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddSessionStats(int linesTyped, int charactersTyped, int timeSeconds, double wpm, double accuracy)
    {
        TotalLinesWritten += linesTyped;
        TotalCharactersTyped += charactersTyped;
        TotalTimeSeconds += timeSeconds;

        var totalSessions = SessionsCompleted + 1;
        AverageWpm = ((AverageWpm * SessionsCompleted) + wpm) / totalSessions;
        AverageAccuracy = ((AverageAccuracy * SessionsCompleted) + accuracy) / totalSessions;

        SessionsCompleted++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStreak(DateTime activityDate)
    {
        var today = activityDate.Date;
        var lastActivity = LastActivityDate?.Date;

        if (lastActivity == null)
        {
            CurrentStreak = 1;
        }
        else if (lastActivity == today)
        {
            return;
        }
        else if (lastActivity == today.AddDays(-1))
        {
            CurrentStreak++;
        }
        else
        {
            CurrentStreak = 1;
        }

        if (CurrentStreak > LongestStreak)
        {
            LongestStreak = CurrentStreak;
        }

        LastActivityDate = today;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UseFreezeToken()
    {
        if (FreezeTokens > 0)
        {
            FreezeTokens--;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void AddFreezeToken(int count = 1)
    {
        FreezeTokens += count;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePreferences(string preferencesJson)
    {
        PreferencesJson = preferencesJson;
        UpdatedAt = DateTime.UtcNow;
    }

    private void UpdateLevel()
    {
        var xpThresholds = new[] { 0, 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500, 5500 };

        for (int i = xpThresholds.Length - 1; i >= 0; i--)
        {
            if (TotalXp >= xpThresholds[i])
            {
                Level = i + 1;
                break;
            }
        }
    }
}
