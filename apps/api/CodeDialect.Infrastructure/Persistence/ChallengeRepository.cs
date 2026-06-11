using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Domain.Entities;
using CodeDialect.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CodeDialect.Infrastructure.Persistence;

public class ChallengeRepository(ApplicationDbContext context) : IChallengeRepository
{
    public async Task<(IReadOnlyList<Challenge> Items, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        Difficulty? difficulty,
        CancellationToken cancellationToken)
    {
        var query = context.Challenges
            .AsNoTracking()
            .Include(c => c.Category)
            .AsQueryable();

        if (difficulty.HasValue)
            query = query.Where(c => c.Difficulty == difficulty.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(c => c.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<Challenge?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Challenges
            .AsNoTracking()
            .Include(c => c.Category)
            .Include(c => c.Implementations)
                .ThenInclude(i => i.Dialect)
                    .ThenInclude(d => d!.Language)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
