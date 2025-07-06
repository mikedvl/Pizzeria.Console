namespace Pizzeria.Application.Interfaces;

public interface IParserFactory
{
    IParser GetParser(string path);
}