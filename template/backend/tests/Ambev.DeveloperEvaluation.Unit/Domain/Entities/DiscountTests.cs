using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Discount entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class DiscountTests
{
    /// <summary>
    /// Tests that when a suspended discount is activated, their status changes to Active.
    /// </summary>
    [Fact(DisplayName = "Discount should be applied for SaleItem")]
    public void Given_Discount_When_TryApply_Then_ShouldBeApplied()
    {
        // Arrange
        var discount = DiscountTestData.GenerateValidDiscount();
        var saleItem = new SaleItem
        {
            Quantity = SaleItemTestData.GenerateQuantityBetween(discount.MinQuantity, discount.MaxQuantity),
            UnitPrice = SaleItemTestData.GenerateUnitPrice()
        };

        // Act
        var result = discount.TryApply(saleItem);

        // Assert
        Assert.True(result);
        Assert.Equal(saleItem.Discount, discount.Percentage);
        Assert.Equal(saleItem.UnitPrice * saleItem.Quantity * (decimal)(1 - saleItem.Discount), saleItem.TotalAmount);
    }

    /// <summary>
    /// Tests that when a suspended discount is activated, their status changes to Active.
    /// </summary>
    [Fact(DisplayName = "Discount should not be applied for SaleItem")]
    public void Given_Discount_When_TryApply_Then_ShouldNotBeApplied()
    {
        // Arrange
        var discount = DiscountTestData.GenerateValidDiscount();
        var saleItem = new SaleItem
        {
            Quantity = SaleItemTestData.GenerateQuantityBetween((short)(discount.MinQuantity - 30), (short)(discount.MinQuantity - 30)),
            UnitPrice = SaleItemTestData.GenerateUnitPrice()
        };

        // Act
        var result = discount.TryApply(saleItem);

        // Assert
        Assert.False(result);
        Assert.Equal(0, saleItem.Discount);
        Assert.Equal(saleItem.UnitPrice * saleItem.Quantity, saleItem.TotalAmount);
    }
}
