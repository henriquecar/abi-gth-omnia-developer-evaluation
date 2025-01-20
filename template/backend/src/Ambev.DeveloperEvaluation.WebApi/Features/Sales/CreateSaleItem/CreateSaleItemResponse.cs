namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleItem;

/// <summary>
/// Represents a sale item to create a new sale in the system.
/// </summary>
public class CreateSaleItemResponse
{
    /// <summary>
    /// The product id reference.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Unit price.
    /// </summary>
    public decimal UnitPrice { get; set; } = decimal.Zero;

    /// <summary>
    /// Quantity.
    /// </summary>
    public short Quantity { get; set; } = 0;

    /// <summary>
    /// Discount.
    /// </summary>
    public double Discount { get; set; } = 0;

    /// <summary>
    /// Total amount.
    /// </summary>
    public decimal TotalAmount { get; set; } = decimal.Zero;
}