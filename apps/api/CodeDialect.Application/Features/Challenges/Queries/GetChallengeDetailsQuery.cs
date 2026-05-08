using CodeDialect.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeDialect.Application.Features.Challenges.Queries;

public record GetChallengeDetailsQuery(Guid Id) : IRequest<ChallengeDetailsDto?>;

public class GetChallengeDetailsQueryHandler : IRequestHandler<GetChallengeDetailsQuery, ChallengeDetailsDto?>
{
    private readonly IApplicationDbContext _context;

    public GetChallengeDetailsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChallengeDetailsDto?> Handle(GetChallengeDetailsQuery request, CancellationToken cancellationToken)
    {
        var challenge = await _context.Challenges
            .AsNoTracking()
            .Include(c => c.Category)
            .Include(c => c.Implementations)
                .ThenInclude(i => i.Dialect)
                    .ThenInclude(d => d!.Language)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (challenge == null) return null;

        return new ChallengeDetailsDto(
            challenge.Id,
            challenge.Title,
            challenge.Description,
            challenge.Difficulty,
            challenge.Category?.Name ?? "Uncategorized",
            challenge.Implementations.Select(i => new ChallengeImplementationDto(
                i.Id,
                i.DialectId,
                i.Dialect?.Name ?? "Unknown",
                i.Dialect?.Language?.Name ?? "Unknown",
                i.StarterCode,
                i.ReferenceSolution,
                i.Dialect?.SyntaxFeatures ?? new()
            )).ToList(),
            challenge.Tags.ToList()
        );
    }
}
