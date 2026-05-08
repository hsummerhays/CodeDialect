namespace CodeDialect.Application.Features.Challenges;

public record SubmitChallengeRequest(Guid DialectId, string Code);
