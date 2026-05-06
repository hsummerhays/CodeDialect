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
    List<ChallengeVersionDto> Versions,
    List<string> Tags);

public record ChallengeVersionDto(
    Guid Id,
    string Name,
    string LanguageName,
    string BoilerplateCode);
