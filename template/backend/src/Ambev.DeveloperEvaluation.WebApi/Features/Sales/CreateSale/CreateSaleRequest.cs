﻿using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new Sale in the system.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// The customer id reference.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Purchased products.
    /// </summary>
    public IList<CreateSaleItemRequest> Items { get; set; } = new List<CreateSaleItemRequest>();
}