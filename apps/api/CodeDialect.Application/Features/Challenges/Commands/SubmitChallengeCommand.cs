using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Domain.Entities;
using CodeDialect.Domain.Enums;
using MediatR;

namespace CodeDialect.Application.Features.Challenges.Commands;

public record SubmitChallengeCommand(Guid ChallengeId, Guid DialectId, string Code) : IRequest<SubmissionResultDto>;

public class SubmitChallengeCommandHandler(ISubmissionRepository repository, ICurrentUserService currentUser)
    : IRequestHandler<SubmitChallengeCommand, SubmissionResultDto>
{
    public async Task<SubmissionResultDto> Handle(
        SubmitChallengeCommand request,
        CancellationToken cancellationToken)
    {
        if (currentUser.UserId is not Guid userId)
            throw new UnauthorizedAccessException("User must be authenticated to submit.");

        var submission = new Submission
        {
            ChallengeId = request.ChallengeId,
            DialectId = request.DialectId,
            SubmittedCode = request.Code,
            Status = SubmissionStatus.Pending,
            UserId = userId
        };

        var saved = await repository.AddAsync(submission, cancellationToken);
        return new SubmissionResultDto(saved.Id, saved.Status.ToString());
    }
}
