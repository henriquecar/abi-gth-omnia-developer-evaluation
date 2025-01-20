using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface IDiscountRepository
{
    /// <summary>
    /// Retrives all discounts
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All discounts</returns>
    Task<IEnumerable<Discount>> ListAll(CancellationToken cancellationToken = default);
}
