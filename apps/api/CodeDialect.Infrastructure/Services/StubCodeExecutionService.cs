using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Domain.Enums;

namespace CodeDialect.Infrastructure.Services;

// Placeholder until the gRPC runner service is stood up.
// Replace with GrpcCodeExecutionService once the runner is available.
public class StubCodeExecutionService : ICodeExecutionService
{
    public Task<ExecutionResult> ExecuteAsync(
        string code, string language, string version, List<TestCase> testCases)
    {
        return Task.FromResult(new ExecutionResult(
            Status: SubmissionStatus.Failed,
            Output: string.Empty,
            ExecutionTimeMs: 0,
            MemoryUsedBytes: 0,
            Results: testCases.Select(tc =>
                new TestCaseResult(false, string.Empty, "Runner not yet available")).ToList()
        ));
    }
}
