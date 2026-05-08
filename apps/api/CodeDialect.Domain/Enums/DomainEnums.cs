namespace CodeDialect.Domain.Enums;

public enum Difficulty
{
    Beginner = 1,
    Intermediate = 2,
    Advanced = 3,
    Expert = 4
}

public enum SubmissionStatus
{
    None = 0,
    Pending = 1,
    Processing = 2,
    Completed = 3,
    Failed = 4,
    TimedOut = 5
}
