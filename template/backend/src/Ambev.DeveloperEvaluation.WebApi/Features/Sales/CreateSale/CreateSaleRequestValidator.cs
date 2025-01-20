using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - BranchId: Required
    /// - CustomerId: Required
    /// - Items: Not empty
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.BranchId).NotEmpty();
        RuleFor(sale => sale.CustomerId).NotEmpty();
        RuleFor(sale => sale.Items).NotEmpty();
        RuleForEach(sale => sale.Items).NotEmpty().SetValidator(new CreateSaleItemRequestValidator());
    }
}