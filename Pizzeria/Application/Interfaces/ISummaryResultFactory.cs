using Pizzeria.Domain.DTO;
using Pizzeria.Domain.Models;

namespace Pizzeria.Application.Interfaces;

public interface ISummaryResultFactory
{
    SummaryResult Create(IEnumerable<Order> orders, IEnumerable<Product> products);
}