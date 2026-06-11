using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeDialect.Infrastructure.Persistence.Configurations;

internal static class JsonValueConverters
{
    internal static ValueConverter<IReadOnlyList<string>, string> StringList() => new(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
        v => (IReadOnlyList<string>)(JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()));

    internal static ValueConverter<Dictionary<string, string>, string> StringDictionary() => new(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
        v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null) ?? new Dictionary<string, string>());
}
