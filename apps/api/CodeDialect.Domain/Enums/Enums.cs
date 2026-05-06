namespace CodeDialect.Domain.Enums;

public enum Difficulty
{
    Beginner,
    Intermediate,
    Advanced,
    Expert
}

public enum SubmissionStatus
{
    Pending,
    Compiling,
    Running,
    Accepted,
    WrongAnswer,
    TimeLimitExceeded,
    MemoryLimitExceeded,
    RuntimeError,
    CompilationError
}
