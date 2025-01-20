using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleItem;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for Sale creation command.
/// </summary>
public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    const short MinItemsPerProduct = 1;
    const short MaxItemsPerProduct = 20;
    const decimal MaxUnitPrice = 20000;

    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProcutId: Required
    /// - Quanty: Required, inclusive between <see cref="MinItemsPerProduct"/> and <see cref="MaxItemsPerProduct"/>
    /// </remarks>
    public CreateSaleItemCommandValidator()
    {
        RuleFor(sale => sale.ProductId).NotEmpty();
        RuleFor(sale => sale.Quantity).InclusiveBetween(MinItemsPerProduct, MaxItemsPerProduct);
        RuleFor(sale => sale.UnitPrice).GreaterThan(decimal.Zero).LessThanOrEqualTo(MaxUnitPrice);
    }
}