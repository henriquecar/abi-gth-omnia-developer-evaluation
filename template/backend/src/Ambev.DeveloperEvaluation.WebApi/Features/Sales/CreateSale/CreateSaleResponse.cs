using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class CreateSaleResponse
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
    /// Purchased products.
    /// </summary>
    public IList<CreateSaleItemResponse> Items { get; set; } = new List<CreateSaleItemResponse>();
}
