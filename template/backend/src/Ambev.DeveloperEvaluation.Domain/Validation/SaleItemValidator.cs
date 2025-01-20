using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public const short MinItemsPerProduct = 1;
    public const short MaxItemsPerProduct = 20;
    public const int MinTotalAmount = 1;
    public const int MaxTotalAmount = 20000;
    public const double MinDiscount = 0;
    public const double MaxDiscount = 1;

    public SaleItemValidator()
    {
        RuleFor(sale => sale.ProductId).NotEmpty().WithMessage("Product cannot be empty.");
        RuleFor(sale => sale.Quantity).InclusiveBetween(MinItemsPerProduct, MaxItemsPerProduct).WithMessage($"Quantity should be between {MinItemsPerProduct} and {MaxItemsPerProduct}.");
        RuleFor(sale => sale.TotalAmount).InclusiveBetween(MinTotalAmount, MaxTotalAmount).WithMessage($"Total amount should be between {MinTotalAmount} and {MaxTotalAmount}.");
        RuleFor(sale => sale.Discount).InclusiveBetween(MinDiscount, MaxDiscount).WithMessage($"Discount should be between {MinTotalAmount} and {MaxDiscount}.");
    }
}
