using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


/// <summary>
/// Represents a discount.
/// This entity follows domain-driven design principles
/// </summary>
public class Discount : BaseEntity
{
    /// <summary>
    /// Minimum quantity.
    /// </summary>
    public short MinQuantity { get; set; }

    /// <summary>
    /// Maximum quantity.
    /// </summary>
    public short MaxQuantity { get; set; }

    /// <summary>
    /// Discount percentage.
    /// </summary>
    public double Percentage { get; set; }

    /// <summary>
    /// Try to apply a discount to sale item
    /// </summary>
    /// <param name="saleItem">The sale item</param>
    /// <returns>True if the discount was applied, otherwise false</returns>
    public bool TryApply(SaleItem saleItem)
    {
        if (saleItem == null) return false;
        if (saleItem.Quantity < MinQuantity || saleItem.Quantity > MaxQuantity)
        {
            saleItem.TotalAmount = saleItem.Quantity * saleItem.UnitPrice;
            return false;
        }
        
        saleItem.Discount = Percentage;
        saleItem.TotalAmount = saleItem.Quantity * saleItem.UnitPrice * (decimal)Percentage;
        return true;
    }
}