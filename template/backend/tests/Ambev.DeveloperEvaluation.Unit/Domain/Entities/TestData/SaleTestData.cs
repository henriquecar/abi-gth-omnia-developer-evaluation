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
public static class SaleTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated users will have valid:
    /// - Quantity (between <see cref="SaleValidator.MinItemsPerProduct"/> and  <see cref="SaleValidator.MaxItemsPerProduct"/>)
    /// - TotalAmount (between <see cref="SaleValidator.MinTotalAmount"/> and  <see cref="SaleValidator.MaxTotalAmount"/>)
    /// - Discount (between 0 and 1)
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(u => u.BranchId, f => f.Random.Guid())
        .RuleFor(u => u.CustomerId, f => f.Random.Guid())
        .RuleFor(u => u.TotalAmount, f => f.Random.Decimal(SaleValidator.MinTotalAmount, SaleValidator.MaxTotalAmount));

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }
}
