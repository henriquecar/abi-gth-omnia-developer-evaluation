namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents the response returned after successfully creating a new Sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Sale,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateSaleItemResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Sale.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created Sale in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// The product id reference.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity.
    /// </summary>
    public short Quantity { get; set; } = 0;

    /// <summary>
    /// Unit price.
    /// </summary>
    public decimal UnitPrice { get; set; } = decimal.Zero;

    /// <summary>
    /// Total sale amount.
    /// </summary>
    public decimal TotalAmount { get; set; } = decimal.Zero;

    /// <summary>
    /// Applied discount.
    /// </summary>
    public double Discount { get; set; } = 0;
}
