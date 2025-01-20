using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleItem;

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
        CreateMap<KeyValuePair<Guid, CreateSaleItemRequest>, CreateSaleItemCommand>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Key));
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
    }
}
