namespace Pizzeria.Infrastructure.Parsers.Interfaces;

public interface IParserFactory
{
    IParser GetParser(string path);
}