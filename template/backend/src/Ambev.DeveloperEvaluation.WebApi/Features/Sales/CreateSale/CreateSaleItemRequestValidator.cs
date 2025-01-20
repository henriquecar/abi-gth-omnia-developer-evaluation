using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<KeyValuePair<Guid, CreateSaleItemRequest>>
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
    public CreateSaleItemRequestValidator()
    {
        RuleFor(sale => sale.Value).NotNull();
        RuleFor(sale => sale.Value.Quantity).InclusiveBetween(MinItemsPerProduct, MaxItemsPerProduct);
        RuleFor(sale => sale.Value.UnitPrice).GreaterThan(decimal.Zero).LessThanOrEqualTo(MaxUnitPrice);
    }
}