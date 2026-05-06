using CodeDialect.Domain.Enums;

namespace CodeDialect.Application.Common.Interfaces;

public interface ICodeExecutionService
{
    Task<ExecutionResult> ExecuteAsync(string code, string language, string version, List<TestCase> testCases);
}

public record ExecutionResult(
    SubmissionStatus Status,
    string Output,
    double ExecutionTimeMs,
    long MemoryUsedBytes,
    List<TestCaseResult> Results);

public record TestCase(string Input, string ExpectedOutput);
public record TestCaseResult(bool Passed, string ActualOutput, string Message);
