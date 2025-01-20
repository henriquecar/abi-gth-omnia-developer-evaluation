using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleItemTestData
{
    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated users will have valid:
    /// - Quantity (between <see cref="SaleItemValidator.MinItemsPerProduct"/> and  <see cref="SaleItemValidator.MaxItemsPerProduct"/>)
    /// - TotalAmount (between <see cref="SaleItemValidator.MinTotalAmount"/> and  <see cref="SaleItemValidator.MaxTotalAmount"/>)
    /// - Discount (between 0 and 1)
    /// </summary>
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(u => u.ProductId, f => f.Random.Guid())
        .RuleFor(u => u.Quantity, f => f.Random.Short(SaleItemValidator.MinItemsPerProduct, SaleItemValidator.MaxItemsPerProduct))
        .RuleFor(u => u.TotalAmount, f => f.Random.Decimal(SaleItemValidator.MinTotalAmount, SaleItemValidator.MaxTotalAmount))
        .RuleFor(u => u.Discount, f => f.Random.Double(0, 1));

    /// <summary>
    /// Generates a valid SaleItem entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid SaleItem entity with randomly generated data.</returns>
    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates a quantity between min and max with randomized value.
    /// </summary>
    /// <returns>A quantity randomly generated value.</returns>
    public static short GenerateQuantityBetween(short minValue, short maxValue)
    {
        return new Faker().Random.Short(minValue, maxValue);
    }

    /// <summary>
    /// Generates a unit price between with randomized value.
    /// </summary>
    /// <returns>A unit price randomly generated value.</returns>
    public static decimal GenerateUnitPrice()
    {
        return new Faker().Random.Decimal(1, 20000);
    }
}
