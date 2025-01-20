using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


/// <summary>
/// Represents a product.
/// This entity follows domain-driven design principles
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Unit price.
    /// </summary>
    public decimal UnitPrice { get; set; }
}