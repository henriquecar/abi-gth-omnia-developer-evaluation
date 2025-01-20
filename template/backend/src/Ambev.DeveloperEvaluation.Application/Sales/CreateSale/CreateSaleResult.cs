
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents the response returned after successfully creating a new Sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Sale,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateSaleResult
{
    /// <summary>
    /// The unique identifier of the created Sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Date when the sale was made.
    /// </summary>
    public DateTime MadeAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the initial status of the Sale account.
    /// </summary>
    public SaleStatus Status { get; set; } = SaleStatus.AwaitingPayment;

    /// <summary>
    /// Total sale amount.
    /// </summary>
    public decimal TotalAmount { get; set; } = decimal.Zero;

    /// <summary>
    /// Gets or sets the list of items.
    /// </summary>
    /// <value>A list of items.</value>
    public IList<CreateSaleItemResult> Items { get; set; } = new List<CreateSaleItemResult>();
}
