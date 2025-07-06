namespace Pizzeria.Domain.DTO;

public record SummaryResult(
    IReadOnlyList<ValidatedOrder> ValidatedOrders,
    IReadOnlyDictionary<string, decimal> TotalIngredients,
    decimal TotalPrice
);