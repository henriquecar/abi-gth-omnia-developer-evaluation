using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IDiscountRepository using Entity Framework Core
/// </summary>
public class DiscountRepository : IDiscountRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of DiscountRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public DiscountRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrives all discounts
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All discounts</returns>
    public async Task<IEnumerable<Discount>> ListAll(CancellationToken cancellationToken = default)
    {
        return await _context.Discounts.ToListAsync();
    }
}
