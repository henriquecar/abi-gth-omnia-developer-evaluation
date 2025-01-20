using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleItemHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale item will have valid:
    /// - ProductId (valid Guid)
    /// - Quantity (from 1 to 3)
    /// - Unit price (from 1 to 2000)
    /// </summary>
    private static readonly Faker<CreateSaleItem> createSaleItemHandlerFaker = new Faker<CreateSaleItem>()
        .RuleFor(u => u.ProductId, f => f.Random.Guid())
        .RuleFor(u => u.Quantity, f => f.Random.Short(1, 3))
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(1, 2000));

    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale item will first discount level:
    /// - ProductId (valid Guid)
    /// - Quantity (from 4 to 9)
    /// - Unit price (from 1 to 2000)
    /// </summary>
    private static readonly Faker<CreateSaleItem> createSaleItemWithFirstDicountLevelHandlerFaker = new Faker<CreateSaleItem>()
        .RuleFor(u => u.ProductId, f => f.Random.Guid())
        .RuleFor(u => u.Quantity, f => f.Random.Short(4, 9))
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(1, 2000));

    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale item will second discount level:
    /// - ProductId (valid Guid)
    /// - Quantity (from 10 to 20)
    /// - Unit price (from 1 to 2000)
    /// </summary>
    private static readonly Faker<CreateSaleItem> createSaleItemWithSecondDicountLevelHandlerFaker = new Faker<CreateSaleItem>()
        .RuleFor(u => u.ProductId, f => f.Random.Guid())
        .RuleFor(u => u.Quantity, f => f.Random.Short(4, 9))
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(1, 2000));

    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale item will negative quantity:
    /// - ProductId (valid Guid)
    /// - Quantity (from <see cref="short.MinValue"> to 0)
    /// - Unit price (from 1 to 2000)
    /// </summary>
    private static readonly Faker<CreateSaleItem> createSaleItemWithNegativeQuantityHandlerFaker = new Faker<CreateSaleItem>()
        .RuleFor(u => u.ProductId, f => f.Random.Guid())
        .RuleFor(u => u.Quantity, f => f.Random.Short(short.MinValue, 0))
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(1, 2000));

    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale item will exceeds quantity:
    /// - ProductId (valid Guid)
    /// - Quantity (from <see cref="SaleItemValidator.MaxItemsPerProduct + 1"> to <see cref="short.MaxValue">)
    /// - Unit price (from 1 to 2000)
    /// </summary>
    private static readonly Faker<CreateSaleItem> createSaleItemExceedsQuantityHandlerFaker = new Faker<CreateSaleItem>()
        .RuleFor(u => u.ProductId, f => f.Random.Guid())
        .RuleFor(u => u.Quantity, f => f.Random.Short(SaleItemValidator.MaxItemsPerProduct + 1, short.MaxValue))
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(1, 2000));

    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale item will exceeds unit price:
    /// - ProductId (valid Guid)
    /// - Quantity (from <see cref="SaleItemValidator.MaxItemsPerProduct + 1"> to <see cref="short.MaxValue">)
    /// - Unit price (from <see cref="SaleItemValidator.MaxItemsPerProduct + 1"> to <see cref="decimal.MaxValue">)
    /// </summary>
    private static readonly Faker<CreateSaleItem> createSaleItemExceedsUnitPriceHandlerFaker = new Faker<CreateSaleItem>()
        .RuleFor(u => u.ProductId, f => f.Random.Guid())
        .RuleFor(u => u.Quantity, f => f.Random.Short(SaleItemValidator.MaxItemsPerProduct + 1, short.MaxValue))
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(SaleItemValidator.MaxUnitPrice + 1, decimal.MaxValue));

    /// <summary>
    /// Generates a valid SaleItem entity with randomized data.
    /// The generated saleItem will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid SaleItem entity with randomly generated data.</returns>
    public static CreateSaleItem GenerateValidCommand()
    {
        return createSaleItemHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a first dicount level SaleItem entity with randomized data.
    /// The generated saleItem will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid SaleItem entity with randomly generated data.</returns>
    public static CreateSaleItem GenerateFirstDicountLevelCommand()
    {
        return createSaleItemWithFirstDicountLevelHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a second discount level SaleItem entity with randomized data.
    /// The generated saleItem will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid SaleItem entity with randomly generated data.</returns>
    public static CreateSaleItem GenerateSecondDiscountLevelCommand()
    {
        return createSaleItemWithSecondDicountLevelHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a negative quantity SaleItem entity with randomized data.
    /// The generated saleItem will have all properties populated with valid values
    /// that meet the system's validation requirements except by quantity.
    /// </summary>
    /// <returns>A invalid SaleItem entity with randomly generated data.</returns>
    public static CreateSaleItem GenerateNegativeQuantityCommand()
    {
        return createSaleItemWithNegativeQuantityHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a exceeds quantity SaleItem entity with randomized data.
    /// The generated saleItem will have all properties populated with valid values
    /// that meet the system's validation requirements except by quantity.
    /// </summary>
    /// <returns>A invalid SaleItem entity with randomly generated data.</returns>
    public static CreateSaleItem GenerateExceedsQuantityCommand()
    {
        return createSaleItemExceedsQuantityHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a exceeds unit price SaleItem entity with randomized data.
    /// The generated saleItem will have all properties populated with valid values
    /// that meet the system's validation requirements except by unit price.
    /// </summary>
    /// <returns>A invalid SaleItem entity with randomly generated data.</returns>
    public static CreateSaleItem GenerateExceedsUnitPriceCommand()
    {
        return createSaleItemExceedsUnitPriceHandlerFaker.Generate();
    }
}
