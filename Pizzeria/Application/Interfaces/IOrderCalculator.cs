using Pizzeria.Domain.DTO;
using Pizzeria.Domain.Models;

namespace Pizzeria.Application.Interfaces;

public interface IOrderCalculator
{
    void Calculate(ValidatedOrder order, IEnumerable<Product> products);
}