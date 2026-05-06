using CodeDialect.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeDialect.Application.Features.Challenges.Queries;

public record GetChallengesQuery : IRequest<List<ChallengeDto>>;

public class GetChallengesQueryHandler : IRequestHandler<GetChallengesQuery, List<ChallengeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetChallengesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ChallengeDto>> Handle(GetChallengesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Challenges
            .Include(c => c.Category)
            .Select(c => new ChallengeDto(
                c.Id,
                c.Title,
                c.Description,
                c.Difficulty,
                c.Category != null ? c.Category.Name : "Uncategorized",
                c.Tags.ToList()))
            .ToListAsync(cancellationToken);
    }
}
