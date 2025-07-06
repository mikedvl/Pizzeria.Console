using Pizzeria.Application.Interfaces;
using Pizzeria.Domain.Models;

namespace Pizzeria.Infrastructure.Parsers.Json;

public class JsonParser : GenericJsonParser,IParser
{
    public async Task<IReadOnlyDictionary<string, List<Ingredient>>> ParseIngredientsAsync(string path)
    {
        return await ParseMapAsync<Ingredient>(path);
    }

    public async Task<IReadOnlyList<Order>> ParseOrderAsync(string path)
    {
        return await ParseListAsync<Order>(path);
    }

    public async Task<IReadOnlyList<Product>> ParseProductAsync(string path)
    {
        return await ParseListAsync<Product>(path);
    }
}