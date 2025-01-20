using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleValidator class.
/// Tests cover validation of all sale properties including salename, email,
/// password, phone, status, and role requirements.
/// </summary>
public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// This test verifies that a sale with valid:
    /// - BranchId (not empty)
    /// - CustomerId (not empty)
    /// - Items (not empty)
    /// - TotalAmount (between <see cref="SaleValidator.MinTotalAmount"> and <see cref="SaleValidator.MaxTotalAmount">)
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid sale should pass all validation rules")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid branchId formats.
    /// This test verifies that branchId that are:
    /// - Empty
    /// fail validation with appropriate error messages.
    /// </summary>
    [Fact(DisplayName = "Invalid branchId formats should fail validation")]
    public void Given_InvalidSaleBranch_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.BranchId = Guid.Empty;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchId);
    }

    /// <summary>
    /// Tests that validation fails for invalid customerId formats.
    /// This test verifies that customerId that are:
    /// - Empty
    /// fail validation with appropriate error messages.
    /// </summary>
    [Fact(DisplayName = "Invalid customerId formats should fail validation")]
    public void Given_InvalidSaleCustomer_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.CustomerId = Guid.Empty;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }

    /// <summary>
    /// Tests that validation fails for invalid items formats.
    /// This test verifies that items that are:
    /// - Empty
    /// fail validation with appropriate error messages.
    /// </summary>
    [Fact(DisplayName = "Invalid items formats should fail validation")]
    public void Given_InvalidSaleItems_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.Items = Enumerable.Empty<Guid>().ToList();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    /// <summary>
    /// Tests that validation fails for invalid total amount formats.
    /// This test verifies that total amount that are:
    /// - Total amount less than minimum
    /// - Total amount greather than maximum
    /// fail validation with appropriate error messages.
    /// </summary>
    /// <param name="totalAmount">The invalid total amount to test.</param>
    [Theory(DisplayName = "Invalid total amount formats should fail validation")]
    [InlineData(SaleValidator.MinTotalAmount - 1)] // less than minimum
    [InlineData(SaleValidator.MaxTotalAmount + 1)] // greather than maximum
    public void Given_InvalidSaleTotalAmount_When_Validated_Then_ShouldHaveError(decimal totalAmount)
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.TotalAmount = totalAmount;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TotalAmount);
    }
}
