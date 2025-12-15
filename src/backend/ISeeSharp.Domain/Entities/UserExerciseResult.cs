namespace ISeeSharp.Domain.Entities;

public class UserExerciseResult : Entity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Guid ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; } = null!;
    public string SubmittedCode { get; private set; } = string.Empty;
    public bool IsCorrect { get; private set; }
    public int ScoreEarned { get; private set; }
    public TimeSpan CompletionTime { get; private set; }
    public int AttemptNumber { get; private set; }

    private UserExerciseResult() { }

    public static UserExerciseResult Create(
        Guid userId,
        Guid exerciseId,
        string submittedCode,
        bool isCorrect,
        int scoreEarned,
        TimeSpan completionTime,
        int attemptNumber)
    {
        return new UserExerciseResult
        {
            UserId = userId,
            ExerciseId = exerciseId,
            SubmittedCode = submittedCode,
            IsCorrect = isCorrect,
            ScoreEarned = scoreEarned,
            CompletionTime = completionTime,
            AttemptNumber = attemptNumber
        };
    }
}
