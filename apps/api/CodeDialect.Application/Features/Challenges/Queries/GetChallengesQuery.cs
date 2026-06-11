using CodeDialect.Application.Common.Interfaces;
using CodeDialect.Application.Common.Models;
using CodeDialect.Domain.Enums;
using MediatR;

namespace CodeDialect.Application.Features.Challenges.Queries;

public record GetChallengesQuery(
    int Page = 1,
    int PageSize = 20,
    Difficulty? Difficulty = null) : IRequest<PaginatedResult<ChallengeDto>>;

public class GetChallengesQueryHandler(IChallengeRepository repository)
    : IRequestHandler<GetChallengesQuery, PaginatedResult<ChallengeDto>>
{
    public async Task<PaginatedResult<ChallengeDto>> Handle(
        GetChallengesQuery request,
        CancellationToken cancellationToken)
    {
        var (items, totalCount) = await repository.GetPagedAsync(
            request.Page, request.PageSize, request.Difficulty, cancellationToken);

        var dtos = items.Select(c => new ChallengeDto(
            c.Id,
            c.Title,
            c.Description,
            c.Difficulty,
            c.Category?.Name ?? "Uncategorized",
            c.Tags.ToList())).ToList();

        return new PaginatedResult<ChallengeDto>(dtos, totalCount, request.Page, request.PageSize);
    }
}
