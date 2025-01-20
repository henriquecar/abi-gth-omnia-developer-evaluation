using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new SaleItem.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a SaleItem, 
/// including ProductId and Quantity. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateSaleItemResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateSaleItemValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateSaleItem : IRequest<CreateSaleItemResult>
{
    /// <summary>
    /// The product id reference.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity.
    /// </summary>
    public short Quantity { get; set; } = 0;

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}