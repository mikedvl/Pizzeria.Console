using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pizzeria.Application.Interfaces;
using Pizzeria.Domain.Models;
using Pizzeria.Infrastructure.Configuration;

namespace Pizzeria.Application.Services;

public class OrderService(
    ISummaryResultFactory summaryResultFactory,
    ISummaryPrinter printer,
    ILogger<OrderService> logger,
    IOptions<DataFileSettings> options,
    IParserFactory parserFactory)
    : IOrderService
{
    private readonly DataFileSettings _paths = options.Value;
    
    public async Task RunAsync()
    {
        logger.LogInformation("Starting order processing");

        try
        {
            var orders = await GetOrdersAsync();
            var products = await GetProductsAsync();
            var summary = summaryResultFactory.Create(orders, products);

            logger.LogInformation("Valid orders: {Count}", summary.ValidatedOrders.Count);

            printer.Print(summary);

            logger.LogInformation("Order processing completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during order processing");
        }
    }

    private async Task<IReadOnlyList<Order>> GetOrdersAsync()
    {
        var parser = parserFactory.GetParser(_paths.Orders);
        return await parser.ParseOrderAsync(_paths.Orders);
    }

    private async Task<IReadOnlyDictionary<string, List<Ingredient>>> GetIngredientsAsync()
    {
        var parser = parserFactory.GetParser(_paths.Ingredients);
        return await parser.ParseIngredientsAsync(_paths.Ingredients);
    }

    private async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        var ingredients = await GetIngredientsAsync();

        var parser = parserFactory.GetParser(_paths.Products);
        var products = await parser.ParseProductAsync(_paths.Products);

        foreach (var product in products)
        {
            if (ingredients.TryGetValue(product.ProductId, out var productIngredients))
            {
                product.Ingredients.AddRange(productIngredients);
            }
        }
        
        return products;
    }
}