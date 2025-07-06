namespace Pizzeria.Models.Dto;

public class ValidatedOrder
{
    public string OrderId { get; init; } = string.Empty;
    public IReadOnlyList<OrderDisplayItem> Items { get; init; } = Array.Empty<OrderDisplayItem>();
    public decimal TotalPrice { get; set; }
    public IReadOnlyDictionary<string, decimal> RequiredIngredients { get; set; } = new Dictionary<string, decimal>();
}