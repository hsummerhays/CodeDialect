using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Domain.Entities;

namespace CodeDialect.Infrastructure.Persistence;

public class SubmissionRepository(ApplicationDbContext context) : ISubmissionRepository
{
    public async Task<Submission> AddAsync(Submission submission, CancellationToken cancellationToken)
    {
        context.Submissions.Add(submission);
        await context.SaveChangesAsync(cancellationToken);
        return submission;
    }
}
