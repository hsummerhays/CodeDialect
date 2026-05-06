using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Domain.Entities;
using CodeDialect.Domain.Enums;
using MediatR;

namespace CodeDialect.Application.Features.Challenges.Commands;

public record SubmitChallengeCommand(Guid ChallengeId, Guid DialectId, string Code) : IRequest<SubmissionResultDto>;

public class SubmitChallengeCommandHandler : IRequestHandler<SubmitChallengeCommand, SubmissionResultDto>
{
    private readonly IApplicationDbContext _context;

    public SubmitChallengeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SubmissionResultDto> Handle(SubmitChallengeCommand request, CancellationToken cancellationToken)
    {
        var submission = new Submission
        {
            ChallengeId = request.ChallengeId,
            DialectId = request.DialectId,
            SubmittedCode = request.Code,
            Status = SubmissionStatus.Pending,
            UserId = Guid.Empty // TODO: resolve from authenticated user context
        };

        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync(cancellationToken);

        return new SubmissionResultDto(submission.Id, submission.Status.ToString());
    }
}
