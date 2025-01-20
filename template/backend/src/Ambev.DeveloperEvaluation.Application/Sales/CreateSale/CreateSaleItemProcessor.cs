using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using System.Collections.Concurrent;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleItemCommand requests
/// </summary>
public class CreateSaleItemProcessor : ICreateSaleItemProcessor
{
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleItemValidator _validator;

    /// <summary>
    /// Initializes a new instance of CreateSaleItemHandler
    /// </summary>
    /// <param name="saleItemRepository">The SaleItem repository</param>
    /// <param name="discountRepository">The discount repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateSaleItemProcessor(ISaleItemRepository saleItemRepository, IDiscountRepository discountRepository, IMapper mapper)
    {
        _saleItemRepository = saleItemRepository;
        _discountRepository = discountRepository;
        _mapper = mapper;
        _validator = new CreateSaleItemValidator();
    }

    /// <summary>
    /// Handles the CreateSaleItemCommand request
    /// </summary>
    /// <param name="command">The CreateSaleItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created SaleItem details</returns>
    public async Task<CreateSaleItemResult> Process(CreateSaleItem command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var saleItem = _mapper.Map<SaleItem>(command);

        var discounts = await _discountRepository.ListAll(cancellationToken);
        foreach (var discount in discounts)
        {
            if (discount.TryApply(saleItem)) break;
        }

        var createdSaleItem = await _saleItemRepository.CreateAsync(saleItem, cancellationToken);
        var result = _mapper.Map<CreateSaleItemResult>(createdSaleItem);
        return result;
    }
}
