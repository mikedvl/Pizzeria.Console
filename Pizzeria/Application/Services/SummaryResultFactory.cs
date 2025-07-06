using Pizzeria.Application.Interfaces;
using Pizzeria.Domain.DTO;
using Pizzeria.Domain.Models;

namespace Pizzeria.Application.Services;

public class SummaryResultFactory(
    IOrderValidator validator,
    IOrderCalculator orderCalculator)
    : ISummaryResultFactory
{
    public SummaryResult Create(IEnumerable<Order> orders, IEnumerable<Product> products)
    {
        var grouped = orders
            .GroupBy(o => o.OrderId)
            .ToList();

        var validatedOrders = new List<ValidatedOrder>();
        var productList = products.ToList();
        
        foreach (var group in grouped)
        {
            var items = group.ToList();

            if (!validator.IsValid(items, productList))
                continue;

            var productMap = productList.ToDictionary(p => p.ProductId);
            var displayItems = items
                .Where(o => productMap.ContainsKey(o.ProductId))
                .Select(o => new OrderDisplayItem(
                    ProductId: o.ProductId,
                    ProductName: productMap[o.ProductId].ProductName,
                    Quantity: o.Quantity,
                    CreatedAt: o.CreatedAt,
                    DeliveryAt: o.DeliveryAt,
                    DeliveryAddress: o.DeliveryAddress
                ))
                .ToList();

            var validated = new ValidatedOrder
            {
                OrderId = group.Key,
                Items = displayItems
            };

            orderCalculator.Calculate(validated, productList);

            validatedOrders.Add(validated);
        }

        var totalIngredients = new Dictionary<string, decimal>();
        decimal totalPrice = 0;

        foreach (var order in validatedOrders)
        {
            totalPrice += order.TotalPrice;

            foreach (var (name, amount) in order.RequiredIngredients)
            {
                totalIngredients.TryAdd(name, 0);
                totalIngredients[name] += amount;
            }
        }

        return new SummaryResult(
            ValidatedOrders: validatedOrders,
            TotalIngredients: totalIngredients,
            TotalPrice: totalPrice
        );
    }
}