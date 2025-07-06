using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Application.Interfaces;
using Pizzeria.Infrastructure.Parsers.Csv;
using Pizzeria.Infrastructure.Parsers.Json;

namespace Pizzeria.Infrastructure.Parsers;

public class ParserFactory(IServiceProvider serviceProvider) : IParserFactory
{
    public IParser GetParser(string path)
    {
        var extension = Path.GetExtension(path).ToLowerInvariant();

        return extension switch
        {
            ".json" => serviceProvider.GetRequiredService<JsonParser>(),
            ".csv"  => serviceProvider.GetRequiredService<CsvParser>(),
            _ => throw new NotSupportedException($"Unsupported file format: {extension}")
        };
    }
}