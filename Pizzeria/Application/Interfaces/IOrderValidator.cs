using Pizzeria.Domain.Models;

namespace Pizzeria.Application.Interfaces;

public interface IOrderValidator
{
    bool IsValid(List<Order> orders, IEnumerable<Product> products);
}