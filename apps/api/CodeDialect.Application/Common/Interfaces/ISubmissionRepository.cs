using CodeDialect.Domain.Entities;

namespace CodeDialect.Application.Common.Interfaces;

public interface ISubmissionRepository
{
    Task<Submission> AddAsync(Submission submission, CancellationToken cancellationToken);
}
