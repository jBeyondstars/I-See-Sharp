namespace ISeeSharp.Domain.Entities;

public class User : Entity
{
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public int TotalScore { get; private set; }
    public int ExercisesCompleted { get; private set; }

    private readonly List<UserExerciseResult> _exerciseResults = [];
    public IReadOnlyCollection<UserExerciseResult> ExerciseResults => _exerciseResults.AsReadOnly();

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

    public void IncrementExercisesCompleted()
    {
        ExercisesCompleted++;
        UpdatedAt = DateTime.UtcNow;
    }
}
