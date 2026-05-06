using CodeDialect.Domain.Entities;
using CodeDialect.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeDialect.Infrastructure.Persistence;

public static class InitialSeed
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        try 
        {
            // Try to ensure DB exists, but ignore if it fails (common in InMemory or restricted envs)
            await context.Database.EnsureCreatedAsync();
        }
        catch { }

        // Standard check to prevent duplicate seeding
        if (await context.Challenges.AnyAsync()) return;

        // --- Categories ---
        var backend = new Category { Name = "Back-End", Description = "Server-side logic and performance" };
        var frontend = new Category { Name = "Front-End", Description = "UI frameworks and state management" };
        var architecture = new Category { Name = "Architecture", Description = "System design and patterns" };
        await context.Categories.AddRangeAsync(backend, frontend, architecture);

        // --- Languages ---
        var csharp = new Language { Name = "C#", Slug = "csharp" };
        var java = new Language { Name = "Java", Slug = "java" };
        var javascript = new Language { Name = "JavaScript", Slug = "javascript" };
        var typescript = new Language { Name = "TypeScript", Slug = "typescript" };
        var python = new Language { Name = "Python", Slug = "python" };
        await context.Languages.AddRangeAsync(csharp, java, javascript, typescript, python);

        // --- Dialects ---
        
        // C#
        var netFramework = new Dialect { Name = ".NET Framework 4.8", Language = csharp, RuntimeVersion = "4.8", Framework = "ASP.NET Web API 2", SyntaxFeatures = new() { "XML Config", "Controllers" } };
        var net10 = new Dialect { Name = ".NET 10", Language = csharp, RuntimeVersion = "10.0", Framework = "ASP.NET Core", SyntaxFeatures = new() { "Minimal APIs", "Primary Constructors", "Native AOT" } };
        
        // Java
        var java8 = new Dialect { Name = "Java 8", Language = java, RuntimeVersion = "1.8", SyntaxFeatures = new() { "Streams", "Lambdas" } };
        var java21 = new Dialect { Name = "Java 21", Language = java, RuntimeVersion = "21", SyntaxFeatures = new() { "Virtual Threads", "Records", "Structured Concurrency" } };
        
        // JS/TS
        var jsLegacy = new Dialect { Name = "Legacy JS", Language = javascript, RuntimeVersion = "ES5", SyntaxFeatures = new() { "Callbacks", "var" } };
        var jsModern = new Dialect { Name = "Modern JS (ESM)", Language = javascript, RuntimeVersion = "ES2023", SyntaxFeatures = new() { "Async/Await", "ES Modules" } };
        var tsModern = new Dialect { Name = "TypeScript 5.x", Language = typescript, RuntimeVersion = "5.4", SyntaxFeatures = new() { "Discriminated Unions", "Mapped Types" } };

        // Python
        var python2 = new Dialect { Name = "Python 2.7", Language = python, RuntimeVersion = "2.7", SyntaxFeatures = new() { "Legacy Print", "ASCII Strings" } };
        var python3 = new Dialect { Name = "Python 3.12", Language = python, RuntimeVersion = "3.12", SyntaxFeatures = new() { "Type Hinting", "Dataclasses", "asyncio" } };

        await context.Dialects.AddRangeAsync(netFramework, net10, java8, java21, jsLegacy, jsModern, tsModern, python2, python3);

        // --- Challenges & Implementations ---

        // 1. Minimal API vs Controller (C#)
        var apiChallenge = new Challenge
        {
            Title = "API Endpoint Modernization",
            Description = "Compare the verbosity of traditional Controller-based routing with modern Minimal APIs.",
            Difficulty = Difficulty.Beginner,
            Category = backend,
            Tags = new() { ".NET", "Architecture", "Minimal API" }
        };

        var apiOld = new ChallengeImplementation
        {
            Challenge = apiChallenge,
            Dialect = netFramework,
            StarterCode = "public class UsersController : ApiController {\n    [HttpGet]\n    public IHttpActionResult Get() { ... }\n}",
            ReferenceSolution = "public class UsersController : ApiController {\n    [Route(\"api/users\")]\n    public IHttpActionResult GetUsers() {\n        return Ok(new { Name = \"John\" });\n    }\n}"
        };

        var apiNew = new ChallengeImplementation
        {
            Challenge = apiChallenge,
            Dialect = net10,
            StarterCode = "var app = builder.Build();\n// Map your endpoint here",
            ReferenceSolution = "app.MapGet(\"/api/users\", () => new { Name = \"John\" });"
        };

        // 2. High-Throughput Concurrency (Java)
        var concurrencyChallenge = new Challenge
        {
            Title = "Scalable Parallel Processing",
            Description = "Handle 10,000 concurrent tasks. Compare OS-thread bound Executors with Virtual Threads.",
            Difficulty = Difficulty.Advanced,
            Category = architecture,
            Tags = new() { "Java", "Loom", "Concurrency" }
        };

        var java8Concurrency = new ChallengeImplementation
        {
            Challenge = concurrencyChallenge,
            Dialect = java8,
            StarterCode = "ExecutorService executor = Executors.newFixedThreadPool(100);",
            ReferenceSolution = "ExecutorService executor = Executors.newFixedThreadPool(100);\nfor (int i = 0; i < 10000; i++) {\n    executor.submit(() -> {\n        Thread.sleep(1000);\n        return \"Done\";\n    });\n}"
        };

        var java21Concurrency = new ChallengeImplementation
        {
            Challenge = concurrencyChallenge,
            Dialect = java21,
            StarterCode = "try (var executor = Executors.new...) { }",
            ReferenceSolution = "try (var executor = Executors.newVirtualThreadPerTaskExecutor()) {\n    IntStream.range(0, 10000).forEach(i -> {\n        executor.submit(() -> {\n            Thread.sleep(1000);\n            return \"Done\";\n        });\n    });\n}"
        };

        // 3. Asynchronous File I/O (JS)
        var fileIoChallenge = new Challenge
        {
            Title = "Non-Blocking File Parsing",
            Description = "Read a large JSON file and parse its contents. Compare callback hell with modern async/await.",
            Difficulty = Difficulty.Intermediate,
            Category = backend,
            Tags = new() { "Node.js", "Async", "FileIO" }
        };

        var jsLegacyIo = new ChallengeImplementation
        {
            Challenge = fileIoChallenge,
            Dialect = jsLegacy,
            StarterCode = "fs.readFile('data.json', function(err, data) { ... });",
            ReferenceSolution = "fs.readFile('data.json', 'utf8', function(err, data) {\n  if (err) throw err;\n  var obj = JSON.parse(data);\n  console.log(obj.name);\n});"
        };

        var jsModernIo = new ChallengeImplementation
        {
            Challenge = fileIoChallenge,
            Dialect = jsModern,
            StarterCode = "const data = await fs.readFile(...);",
            ReferenceSolution = "import fs from 'fs/promises';\nconst data = await fs.readFile('data.json', 'utf8');\nconst { name } = JSON.parse(data);\nconsole.log(name);"
        };

        // 4. Dependency Injection (C# Modern)
        var diChallenge = new Challenge
        {
            Title = "Modern Dependency Injection",
            Description = "Implement a service that requires a repository. Compare manual instantiation with Primary Constructors.",
            Difficulty = Difficulty.Intermediate,
            Category = architecture,
            Tags = new() { "C#", "Design Patterns", "DI" }
        };

        var diOld = new ChallengeImplementation
        {
            Challenge = diChallenge,
            Dialect = netFramework,
            StarterCode = "public class UserService {\n    private readonly IUserRepository _repo;\n    public UserService() { ... }\n}",
            ReferenceSolution = "public class UserService {\n    private readonly IUserRepository _repo;\n    public UserService(IUserRepository repo) {\n        _repo = repo;\n    }\n}"
        };

        var diNew = new ChallengeImplementation
        {
            Challenge = diChallenge,
            Dialect = net10,
            StarterCode = "public class UserService(...) { }",
            ReferenceSolution = "public class UserService(IUserRepository repo) {\n    public async Task<User> Get() => await repo.GetAsync();\n}"
        };

        // 5. Type-Safe API Responses (TS)
        var typeSafeChallenge = new Challenge
        {
            Title = "Discriminated Union Responses",
            Description = "Handle an API response that can be Success or Error. Use TS features to ensure type safety.",
            Difficulty = Difficulty.Advanced,
            Category = frontend,
            Tags = new() { "TypeScript", "Type Safety", "Unions" }
        };

        var tsSafetyImpl = new ChallengeImplementation
        {
            Challenge = typeSafeChallenge,
            Dialect = tsModern,
            StarterCode = "type ApiResponse = Success | Error;",
            ReferenceSolution = "interface Success { status: 'success'; data: string; }\ninterface Error { status: 'error'; message: string; }\ntype ApiResponse = Success | Error;\n\nfunction handle(res: ApiResponse) {\n  if (res.status === 'success') {\n    console.log(res.data);\n  } else {\n    console.error(res.message);\n  }\n}"
        };

        // 6. Data Modeling Evolution (Python)
        var pythonDataChallenge = new Challenge
        {
            Title = "Modern Data Modeling",
            Description = "Model a 'Project' with name and deadline. Compare raw dictionaries with Dataclasses.",
            Difficulty = Difficulty.Beginner,
            Category = backend,
            Tags = new() { "Python", "Dataclasses", "Type Hinting" }
        };

        var py2Impl = new ChallengeImplementation
        {
            Challenge = pythonDataChallenge,
            Dialect = python2,
            StarterCode = "project = {'name': 'Legacy', 'days': 10}",
            ReferenceSolution = "project = {'name': 'CodeDialect', 'deadline': '2026-12-31'}\nprint 'Project:', project['name']"
        };

        var py3Impl = new ChallengeImplementation
        {
            Challenge = pythonDataChallenge,
            Dialect = python3,
            StarterCode = "@dataclass\nclass Project: ...",
            ReferenceSolution = "from dataclasses import dataclass\n@dataclass\nclass Project:\n    name: string\n    deadline: datetime\n\nproj = Project('CodeDialect', datetime.now())"
        };

        await context.Challenges.AddRangeAsync(apiChallenge, concurrencyChallenge, fileIoChallenge, diChallenge, typeSafeChallenge, pythonDataChallenge);
        await context.Implementations.AddRangeAsync(apiOld, apiNew, java8Concurrency, java21Concurrency, jsLegacyIo, jsModernIo, diOld, diNew, tsSafetyImpl, py2Impl, py3Impl);

        await context.SaveChangesAsync();
    }
}
