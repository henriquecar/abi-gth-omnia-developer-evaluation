using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateSaleItem responses
/// </summary>
public class CreateSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSaleItem feature
    /// </summary>
    public CreateSaleItemProfile()
    {
        CreateMap<CreateSaleItemRequest, CreateSaleItem>();
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
    }
}
