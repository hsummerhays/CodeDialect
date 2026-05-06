using CodeDialect.Application.Features.Challenges;
using CodeDialect.Application.Features.Challenges.Commands;
using CodeDialect.Application.Features.Challenges.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeDialect.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChallengesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChallengesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ChallengeDto>>> GetChallenges()
    {
        return await _mediator.Send(new GetChallengesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChallengeDetailsDto>> GetChallenge(Guid id)
    {
        var result = await _mediator.Send(new GetChallengeDetailsQuery(id));
        if (result == null) return NotFound();
        return result;
    }

    [HttpPost("{id}/submissions")]
    public async Task<ActionResult<SubmissionResultDto>> SubmitChallenge(Guid id, [FromBody] SubmitChallengeRequest request)
    {
        var result = await _mediator.Send(new SubmitChallengeCommand(id, request.DialectId, request.Code));
        return Accepted(result);
    }
}
