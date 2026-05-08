using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Application.Common.Models;
using CodeDialect.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeDialect.Application.Features.Challenges.Queries;

public record GetChallengesQuery(
    int Page = 1,
    int PageSize = 20,
    Difficulty? Difficulty = null) : IRequest<PaginatedResult<ChallengeDto>>;

public class GetChallengesQueryHandler : IRequestHandler<GetChallengesQuery, PaginatedResult<ChallengeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetChallengesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<ChallengeDto>> Handle(GetChallengesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Challenges
            .AsNoTracking()
            .Include(c => c.Category)
            .AsQueryable();

        if (request.Difficulty.HasValue)
            query = query.Where(c => c.Difficulty == request.Difficulty.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(c => c.Title)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new ChallengeDto(
                c.Id,
                c.Title,
                c.Description,
                c.Difficulty,
                c.Category != null ? c.Category.Name : "Uncategorized",
                c.Tags.ToList()))
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ChallengeDto>(items, totalCount, request.Page, request.PageSize);
    }
}
