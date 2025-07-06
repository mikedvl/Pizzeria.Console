using Pizzeria.Models;
using Pizzeria.Models.Dto;

namespace Pizzeria.Services.Interfaces;

public interface ISummaryResultFactory
{
    SummaryResult Create(IEnumerable<Order> orders, IEnumerable<Product> products);
}