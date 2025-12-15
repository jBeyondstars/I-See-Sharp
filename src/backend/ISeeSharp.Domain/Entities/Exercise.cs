namespace ISeeSharp.Domain.Entities;

public class Exercise : Entity
{
    public string Title { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Instructions { get; private set; } = string.Empty;
    public string StarterCode { get; private set; } = string.Empty;
    public string Solution { get; private set; } = string.Empty;
    public ExerciseDifficulty Difficulty { get; private set; }
    public ExerciseCategory Category { get; private set; }
    public int Points { get; private set; }
    public bool IsActive { get; private set; } = true;

    private readonly List<UserExerciseResult> _userResults = [];
    public IReadOnlyCollection<UserExerciseResult> UserResults => _userResults.AsReadOnly();

    private Exercise() { }

    public static Exercise Create(
        string title,
        string slug,
        string description,
        string instructions,
        string starterCode,
        string solution,
        ExerciseDifficulty difficulty,
        ExerciseCategory category,
        int points)
    {
        return new Exercise
        {
            Title = title,
            Slug = slug,
            Description = description,
            Instructions = instructions,
            StarterCode = starterCode,
            Solution = solution,
            Difficulty = difficulty,
            Category = category,
            Points = points
        };
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum ExerciseDifficulty
{
    Beginner,
    Intermediate,
    Advanced,
    Expert
}

public enum ExerciseCategory
{
    Syntax,
    Methods,
    Classes,
    Interfaces,
    Generics,
    Linq,
    AsyncAwait,
    Collections,
    ErrorHandling,
    Patterns
}
