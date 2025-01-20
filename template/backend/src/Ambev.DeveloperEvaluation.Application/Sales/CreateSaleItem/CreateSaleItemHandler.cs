using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using System.Collections.Concurrent;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleItem;

/// <summary>
/// Handler for processing CreateSaleItemCommand requests
/// </summary>
public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
{
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of CreateSaleItemHandler
    /// </summary>
    /// <param name="saleItemRepository">The SaleItem repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="mediator">The mediator instance</param>
    public CreateSaleItemHandler(ISaleItemRepository saleItemRepository, IMapper mapper, IMediator mediator, IDiscountRepository discountRepository)
    {
        _saleItemRepository = saleItemRepository;
        _mapper = mapper;
        _mediator = mediator;
        _discountRepository = discountRepository;
    }

    /// <summary>
    /// Handles the CreateSaleItemCommand request
    /// </summary>
    /// <param name="command">The CreateSaleItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created SaleItem details</returns>
    public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

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
