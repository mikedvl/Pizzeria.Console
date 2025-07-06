using Pizzeria.Models;
using Pizzeria.Services.Interfaces;

namespace Pizzeria.Services;

public class OrderValidator : IOrderValidator
{
    public bool IsValid(List<Order> orders, IEnumerable<Product> products)
    {
        if (orders.Count == 0) return false;

        var orderId = orders[0].OrderId;
        var productList = products.ToList();
        
        if (productList.Count == 0) return false;
        
        foreach (var order in orders)
        {
            if (string.IsNullOrWhiteSpace(order.OrderId)) return false;
            
            if (order.OrderId != orderId) return false;
            if (order.Quantity <= 0) return false;
            if (order.DeliveryAt < order.CreatedAt) return false;
            if (string.IsNullOrWhiteSpace(order.ProductId)) return false;
            if (productList.All(p => p.ProductId != order.ProductId)) return false;
            
            var product = productList.FirstOrDefault(p => p.ProductId == order.ProductId);
            if (product == null) return false;
            if (string.IsNullOrWhiteSpace(product.ProductId)) return false;
            if (string.IsNullOrWhiteSpace(product.ProductName)) return false;
            if (product.Price <= 0) return false;
            if (product.Ingredients.Count == 0) return false;
        }
        
        return true;
    }
}
