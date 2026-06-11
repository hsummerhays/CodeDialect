using CodeDialect.Application.Common.Models;
using CodeDialect.Domain.Entities;
using CodeDialect.Domain.Enums;

namespace CodeDialect.Application.Common.Interfaces;

public interface IChallengeRepository
{
    Task<(IReadOnlyList<Challenge> Items, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        Difficulty? difficulty,
        CancellationToken cancellationToken);

    Task<Challenge?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken);
}
