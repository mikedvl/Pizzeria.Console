using Pizzeria.Models;

namespace Pizzeria.Services.Interfaces;

public interface IOrderValidator
{
    bool IsValid(List<Order> orders, IEnumerable<Product> products);
}