using CodeDialect.Domain.Enums;

namespace CodeDialect.Application.Features.Challenges;

public record ChallengeDto(
    Guid Id,
    string Title,
    string Description,
    Difficulty Difficulty,
    string CategoryName,
    List<string> Tags);

public record ChallengeDetailsDto(
    Guid Id,
    string Title,
    string Description,
    Difficulty Difficulty,
    string CategoryName,
    List<ChallengeImplementationDto> Implementations,
    List<string> Tags);

public record ChallengeImplementationDto(
    Guid Id,
    Guid DialectId,
    string DialectName,
    string LanguageName,
    string StarterCode);
