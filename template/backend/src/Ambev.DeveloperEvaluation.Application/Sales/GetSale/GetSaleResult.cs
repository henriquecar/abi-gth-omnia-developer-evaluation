using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResult
{
    /// <summary>
    /// Date when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Customer Id.
    /// Required.
    /// </summary>
    public Guid CustomerId { get; set; }

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
    /// Indicates whether the sale is awaiting payment, awaiting transport, transportins, completed or cancelled
    /// </summary>
    public SaleStatus Status { get; set; } = SaleStatus.AwaitingPayment;
}
