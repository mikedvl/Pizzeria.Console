namespace Pizzeria.Domain.Models;

public record Product(
    string ProductId,
    string ProductName,
    decimal Price
)
{
    public List<Ingredient> Ingredients { get; init; } = [];
}