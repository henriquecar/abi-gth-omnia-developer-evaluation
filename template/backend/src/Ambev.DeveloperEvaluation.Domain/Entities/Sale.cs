using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


/// <summary>
/// Represents a sale in the system with items.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Date when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date when the sale was made.
    /// Null for draft sales.
    /// </summary>
    public DateTime? MadeAt { get; set; }

    /// <summary>
    /// Customer Id.
    /// Required.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Purchased products.
    /// </summary>
    public IList<Guid> Items { get; set; } = new List<Guid>();

    /// <summary>
    /// Total sale amount.
    /// Must be greather than <see cref="decimal.Zero"/>.
    /// </summary>
    public decimal TotalAmount { get; set; } = decimal.Zero;

    /// <summary>
    /// Branch where the sale was made.
    /// Required.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets the sale's current status.
    /// Indicates whether the sale is draft, awaiting payment, awaiting transport, transportins, completed or cancelled
    /// </summary>
    public SaleStatus Status { get; set; } = SaleStatus.Draft;

    /// <summary>
    /// Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">BranchId validity</list>
    /// <list type="bullet">CustomerId validity</list>
    /// <list type="bullet">Items validity</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}