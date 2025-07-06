using Pizzeria.Domain.Models;

namespace Pizzeria.Application.Interfaces;

public interface IParser
{
    Task<IReadOnlyDictionary<string, List<Ingredient>>> ParseIngredientsAsync(string path);
    Task<IReadOnlyList<Order>> ParseOrderAsync(string path);
    Task<IReadOnlyList<Product>> ParseProductAsync(string path);
}