using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleItemValidator class.
/// Tests cover validation of all saleItem properties including saleItemname, email,
/// password, phone, status, and role requirements.
/// </summary>
public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator;

    public SaleItemValidatorTests()
    {
        _validator = new SaleItemValidator();
    }

    /// <summary>
    /// Tests that validation passes when all saleItem properties are valid.
    /// This test verifies that a saleItem with valid:
    /// - Quantity (between <see cref="SaleItemValidator.MinItemsPerProduct"/> and  <see cref="SaleItemValidator.MaxItemsPerProduct"/>)
    /// - TotalAmount (between <see cref="SaleItemValidator.MinTotalAmount"/> and  <see cref="SaleItemValidator.MaxTotalAmount"/>)
    /// - Discount (between 0 and 1)
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid saleItem should pass all validation rules")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid productId formats.
    /// This test verifies that productId that are:
    /// - Empty
    /// fail validation with appropriate error messages.
    /// </summary>
    [Fact(DisplayName = "Invalid productId formats should fail validation")]
    public void Given_InvalidSaleItemProduct_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();
        saleItem.ProductId = Guid.Empty;

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    /// <summary>
    /// Tests that validation fails for invalid quantity formats.
    /// This test verifies that quantity that are:
    /// - Quantity less than minimum
    /// - Quantity greather than maximum
    /// fail validation with appropriate error messages.
    /// </summary>
    /// <param name="quantity">The invalid quantity to test.</param>
    [Theory(DisplayName = "Invalid quantity formats should fail validation")]
    [InlineData(SaleItemValidator.MinItemsPerProduct - 1)] // less than minimum
    [InlineData(SaleItemValidator.MaxItemsPerProduct + 1)] // greather than maximum
    public void Given_InvalidSaleItemQuantity_When_Validated_Then_ShouldHaveError(short quantity)
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();
        saleItem.Quantity = quantity;

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    /// <summary>
    /// Tests that validation fails for invalid discount formats.
    /// This test verifies that discount that are:
    /// - Discount less than minimum
    /// - Discount greather than maximum
    /// fail validation with appropriate error messages.
    /// </summary>
    /// <param name="discount">The invalid discount to test.</param>
    [Theory(DisplayName = "Invalid discount formats should fail validation")]
    [InlineData(SaleItemValidator.MinDiscount - 1)] // less than minimum
    [InlineData(SaleItemValidator.MaxDiscount + 1)] // greather than maximum
    public void Given_InvalidSaleItemDiscount_When_Validated_Then_ShouldHaveError(double discount)
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();
        saleItem.Discount = discount;

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Discount);
    }
}
