using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for Sale creation command.
/// </summary>
public class CreateSaleItemValidator : AbstractValidator<CreateSaleItem>
{
    const short MinItemsPerProduct = 1;
    const short MaxItemsPerProduct = 20;

    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProcutId: Required
    /// - Quanty: Required, inclusive between <see cref="MinItemsPerProduct"/> and <see cref="MaxItemsPerProduct"/>
    /// </remarks>
    public CreateSaleItemValidator()
    {
        RuleFor(sale => sale.ProductId).NotEmpty();
        RuleFor(sale => sale.Quantity).InclusiveBetween(MinItemsPerProduct, MaxItemsPerProduct);
    }
}