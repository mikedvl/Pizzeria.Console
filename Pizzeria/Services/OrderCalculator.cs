using Pizzeria.Models;
using Pizzeria.Models.Dto;
using Pizzeria.Services.Interfaces;

namespace Pizzeria.Services;

public class OrderCalculator : IOrderCalculator
{
    public void Calculate(ValidatedOrder order, IEnumerable<Product> products)
    {
        decimal total = 0;
        var requiredIngredients = new Dictionary<string, decimal>();
        var productList = products.ToList();
        
        foreach (var item in order.Items)
        {
            var product = productList.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (product == null) continue;

            total += product.Price * item.Quantity;

            foreach (var ing in product.Ingredients)
            {
                if (!requiredIngredients.TryAdd(ing.Name, ing.Amount * item.Quantity))
                {
                    requiredIngredients[ing.Name] += ing.Amount * item.Quantity;
                }
            }
        }

        order.TotalPrice = total;
        order.RequiredIngredients = requiredIngredients;
    }

    public Dictionary<string, decimal> CalculateTotalIngredients(IEnumerable<ValidatedOrder> orders)
    {
        var totalIngredients = new Dictionary<string, decimal>();

        foreach (var order in orders)
        {
            foreach (var (name, amount) in order.RequiredIngredients)
            {
                if (!totalIngredients.TryAdd(name, amount))
                {
                    totalIngredients[name] += amount;
                }
            }
        }

        return totalIngredients;
    }
}