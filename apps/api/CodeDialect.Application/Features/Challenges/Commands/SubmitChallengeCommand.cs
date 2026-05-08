using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Domain.Entities;
using CodeDialect.Domain.Enums;
using MediatR;

namespace CodeDialect.Application.Features.Challenges.Commands;

public record SubmitChallengeCommand(Guid ChallengeId, Guid DialectId, string Code) : IRequest<SubmissionResultDto>;

public class SubmitChallengeCommandHandler : IRequestHandler<SubmitChallengeCommand, SubmissionResultDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public SubmitChallengeCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<SubmissionResultDto> Handle(SubmitChallengeCommand request, CancellationToken cancellationToken)
    {
        var submission = new Submission
        {
            ChallengeId = request.ChallengeId,
            DialectId = request.DialectId,
            SubmittedCode = request.Code,
            Status = SubmissionStatus.Pending,
            UserId = _currentUser.UserId!.Value
        };

        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync(cancellationToken);

        return new SubmissionResultDto(submission.Id, submission.Status.ToString());
    }
}
