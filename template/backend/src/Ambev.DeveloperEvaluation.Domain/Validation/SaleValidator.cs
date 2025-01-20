using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public const int MinTotalAmount = 1;
    public const int MaxTotalAmount = 200000;

    public SaleValidator()
    {
        RuleFor(sale => sale.BranchId).NotEmpty().WithMessage("Branch where the sale was made cannot be empty.");
        RuleFor(sale => sale.CustomerId).NotEmpty().WithMessage("Customer cannot be empty.");
        RuleFor(sale => sale.Items).NotEmpty().WithMessage("Items cannot be empty.");
        RuleFor(sale => sale.TotalAmount).InclusiveBetween(MinTotalAmount, MaxTotalAmount).WithMessage($"Total amount should be between {MinTotalAmount} and {MaxTotalAmount}.");
    }
}
