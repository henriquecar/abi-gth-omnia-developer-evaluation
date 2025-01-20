using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DiscountTestData
{
    /// <summary>
    /// Configures the Faker to generate valid discount entities.
    /// The generated discounts will have valid:
    /// - MinQuantity (from 1 to 30)
    /// - MaxQuantity (from 31 to 50)
    /// - Percentage (percentage from 10% to 90%)
    /// </summary>
    private static readonly Faker<Discount> DiscountFaker = new Faker<Discount>()
        .RuleFor(u => u.MinQuantity, f => f.Random.Short(1, 30))
        .RuleFor(u => u.MaxQuantity, f => f.Random.Short(31, 50))
        .RuleFor(u => u.Percentage, f => f.Random.Double(.1, .9));

    /// <summary>
    /// Generates a valid Discount entity with randomized data.
    /// The generated discount will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Discount entity with randomly generated data.</returns>
    public static Discount GenerateValidDiscount()
    {
        return DiscountFaker.Generate();
    }
}
