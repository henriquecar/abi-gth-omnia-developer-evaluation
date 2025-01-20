using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Discount entity operations
/// </summary>
public interface IDiscountRepository
{
    /// <summary>
    /// Retrives all discounts
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All discounts</returns>
    Task<IEnumerable<Discount>> ListAllAsync(CancellationToken cancellationToken = default);
}
