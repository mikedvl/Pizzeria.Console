namespace Pizzeria.Models.Dto;

public record SummaryResult(
    IReadOnlyList<ValidatedOrder> ValidatedOrders,
    IReadOnlyDictionary<string, decimal> TotalIngredients,
    decimal TotalPrice
);