using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public const short MinItemsPerProduct = 1;
    public const short MaxItemsPerProduct = 20;
    public const decimal MaxUnitPrice = 20000;

    public SaleItemValidator()
    {
        RuleFor(sale => sale.ProductId).NotEmpty().WithMessage("Product cannot be empty.");
        RuleFor(sale => sale.Quantity).InclusiveBetween(MinItemsPerProduct, MaxItemsPerProduct).WithMessage($"Quantity should be between {MinItemsPerProduct} and {MaxItemsPerProduct}.");
        RuleFor(sale => sale.UnitPrice)
            .GreaterThan(decimal.Zero).WithMessage($"Unit price should be greather than {decimal.Zero}.")
            .LessThanOrEqualTo(MaxUnitPrice).WithMessage($"Unit price should be less than or equal to {MaxUnitPrice}.");
        RuleFor(sale => sale.TotalAmount).GreaterThan(decimal.Zero).WithMessage($"Total amount should be greather than {decimal.Zero}.");
        RuleFor(sale => sale.Discount).GreaterThan((short)decimal.Zero).WithMessage($"Discount should be greather than {decimal.Zero}.");
    }
}
