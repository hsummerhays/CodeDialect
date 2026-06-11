using CodeDialect.Application.Common.Exceptions;
using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Domain.Entities;
using MediatR;

namespace CodeDialect.Application.Features.Challenges.Queries;

public record GetChallengeDetailsQuery(Guid Id) : IRequest<ChallengeDetailsDto>;

public class GetChallengeDetailsQueryHandler(IChallengeRepository repository)
    : IRequestHandler<GetChallengeDetailsQuery, ChallengeDetailsDto>
{
    public async Task<ChallengeDetailsDto> Handle(
        GetChallengeDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var challenge = await repository.GetByIdWithDetailsAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Challenge), request.Id);

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
                i.Dialect?.SyntaxFeatures.ToList() ?? new()
            )).ToList(),
            challenge.Tags.ToList()
        );
    }
}
