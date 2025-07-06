using Pizzeria.Models;
using Pizzeria.Services;
using Pizzeria.Services.Interfaces;

namespace Pizzeria.Tests.Unit.Validators;

/// <summary>
/// Unit tests for the OrderValidator class.
/// Verifies different validation scenarios for order and product data integrity.
/// </summary>
public class OrderValidatorTests
{
    private readonly IOrderValidator _validator = new OrderValidator();

    [Fact]
    public void IsValid_WithValidData_ReturnsTrue()
    {
        // Arrange
        // Create two valid orders with the same OrderId and valid timestamps.
        var orders = new List<Order>
        {
            new("ORD1", "PZ1", 2, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1), "Street 1"),
            new("ORD1", "PZ1", 1, DateTime.Now.AddHours(-2), DateTime.Now.AddHours(2), "Street 1")
        };

        // Create a valid product that matches the ProductId in the orders.
        var products = new List<Product>
        {
            new("PZ1", "Margarita", 10m)
            {
                Ingredients =
                [
                    new Ingredient("Cheese", 0.5m),
                    new Ingredient("Tomato", 0.3m)
                ]
            }
        };

        // Act
        var result = _validator.IsValid(orders, products);

        // Assert
        Assert.True(result); // Expect validation to succeed
    }
    
    [Fact]
    public void IsValid_WithProductHavingEmptyId_ReturnsFalse()
    {
        // Arrange
        // Order refers to a product with ID "PZ_EMPTY", which is matched in the product list
        var orders = new List<Order>
        {
            new("ORD_INVALID", "PZ_EMPTY", 1, DateTime.Now.AddMinutes(-30), DateTime.Now.AddMinutes(30), "Fake Street")
        };

        // The product exists but has an invalid (empty) ProductId
        var productWithEmptyId = new Product("", "Faulty Pizza", 9.99m)
        {
            Ingredients =
            [
                new Ingredient("Cheese", 0.5m),
                new Ingredient("Tomato", 0.3m)
            ]
        };

        var products = new List<Product> { productWithEmptyId };

        // Act
        var result = _validator.IsValid(orders, products);

        // Assert
        Assert.False(result); // Expect validation to fail due to product having empty ProductId
    }

    [Fact]
    public void IsValid_WithInvalidProductData_ReturnsFalse()
    {
        // Arrange
        // Order references a product that exists but has invalid fields
        var orders = new List<Order>
        {
            new("ORD3", "PZ3", 1, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1), "Street 3")
        };

        // Product is missing a name, has zero price
        var products = new List<Product>
        {
            new("PZ3", "", 0)
            {
                Ingredients =
                [
                    new Ingredient("Cheese", 0.5m),
                    new Ingredient("Tomato", 0.3m)
                ]
            }
        };

        // Act
        var result = _validator.IsValid(orders, products);

        // Assert
        Assert.False(result); // Expect validation to fail due to invalid product data
    }
    
    
    [Fact]
    public void IsValid_WithProductMissingIngredients_ReturnsFalse()
    {
        // Arrange
        // Order refers to a valid product, but the product has no ingredients
        var orders = new List<Order>
        {
            new("ORD_NO_ING", "PZ100", 1, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1), "Valid Street")
        };

        var products = new List<Product>
        {
            new("PZ100", "Plain Pizza", 12.50m)
            {
                Ingredients = [] // Empty ingredient list
            }
        };

        // Act
        var result = _validator.IsValid(orders, products);

        // Assert
        Assert.False(result); // Validation should fail due to missing ingredients
    }
}