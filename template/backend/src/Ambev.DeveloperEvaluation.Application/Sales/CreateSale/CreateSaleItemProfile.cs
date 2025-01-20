using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between SaleItem entity and CreateSaleItemResponse
/// </summary>
public class CreateSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSaleItem operation
    /// </summary>
    public CreateSaleItemProfile()
    {
        CreateMap<CreateSaleItem, SaleItem>();
        CreateMap<SaleItem, CreateSaleItemResult>();
    }
}
