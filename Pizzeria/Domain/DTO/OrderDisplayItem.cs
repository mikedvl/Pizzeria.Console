namespace Pizzeria.Domain.DTO;

public record OrderDisplayItem(
    string ProductId,
    string ProductName,
    int Quantity,
    DateTime CreatedAt,
    DateTime DeliveryAt,
    string DeliveryAddress
);