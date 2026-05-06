using CodeDialect.Domain.Entities;
using CodeDialect.Domain.Enums;
using CodeDialect.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeDialect.Infrastructure.Persistence;

public static class InitialSeed
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.EnsureCreatedAsync();

        if (await context.Challenges.AnyAsync()) return;

        var backend = new ChallengeCategory { Name = "Back-End" };
        var frontend = new ChallengeCategory { Name = "Front-End" };
        var infra = new ChallengeCategory { Name = "Infrastructure" };

        var dotNet = new ChallengeLanguage { Name = ".NET", Slug = "dotnet" };
        var java = new ChallengeLanguage { Name = "Java", Slug = "java" };
        var react = new ChallengeLanguage { Name = "React", Slug = "react" };

        var dotNet10 = new ChallengeVersion { Name = ".NET 10", Language = dotNet };
        var dotNet8 = new ChallengeVersion { Name = ".NET 8", Language = dotNet };
        var java21 = new ChallengeVersion { Name = "Java 21", Language = java };
        var reactHooks = new ChallengeVersion { Name = "React Hooks", Language = react };

        var challenge1 = new Challenge
        {
            Title = "String Interpolation Performance",
            Description = "Compare string interpolation handlers in .NET 10.",
            Difficulty = Difficulty.Intermediate,
            Category = backend,
            Tags = new List<string> { ".NET", "C#", "Performance" },
            Versions = new List<ChallengeVersion> { dotNet10, dotNet8 }
        };

        await context.Categories.AddRangeAsync(backend, frontend, infra);
        await context.Languages.AddRangeAsync(dotNet, java, react);
        await context.Challenges.AddAsync(challenge1);

        await context.SaveChangesAsync();
    }
}
