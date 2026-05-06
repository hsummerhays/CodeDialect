using CodeDialect.Application.Features.Challenges;
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
}
