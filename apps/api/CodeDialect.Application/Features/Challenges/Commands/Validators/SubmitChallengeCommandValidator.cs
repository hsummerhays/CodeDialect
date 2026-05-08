using CodeDialect.Application.Features.Challenges.Commands;
using FluentValidation;

namespace CodeDialect.Application.Features.Challenges.Commands.Validators;

public class SubmitChallengeCommandValidator : AbstractValidator<SubmitChallengeCommand>
{
    public SubmitChallengeCommandValidator()
    {
        RuleFor(x => x.ChallengeId).NotEmpty();
        RuleFor(x => x.DialectId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty().MaximumLength(50_000);
    }
}
