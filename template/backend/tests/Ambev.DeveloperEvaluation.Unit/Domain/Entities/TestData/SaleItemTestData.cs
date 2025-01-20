using Ambev.DeveloperEvaluation.Domain.Entities;
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
