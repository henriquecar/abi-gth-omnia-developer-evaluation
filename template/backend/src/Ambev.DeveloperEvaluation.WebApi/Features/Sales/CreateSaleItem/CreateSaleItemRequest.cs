namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleItem;

/// <summary>
/// Represents a sale item to create a new sale in the system.
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// Quantity.
    /// </summary>
    public short Quantity { get; set; } = 0;

    /// <summary>
    /// Unit price.
    /// </summary>
    public decimal UnitPrice { get; set; } = decimal.Zero;
}