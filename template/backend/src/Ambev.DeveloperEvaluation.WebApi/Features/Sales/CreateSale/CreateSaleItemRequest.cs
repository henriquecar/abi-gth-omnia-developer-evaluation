namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a sale item to create a new sale in the system.
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// The product id reference.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity.
    /// </summary>
    public short Quantity { get; set; } = 0;
}