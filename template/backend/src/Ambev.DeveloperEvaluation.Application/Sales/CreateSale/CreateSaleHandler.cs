﻿using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using System.Collections.Concurrent;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICreateSaleItemProcessor _createSaleItemProcessor;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The Sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateSaleHandler(ISaleRepository saleRepository, ICreateSaleItemProcessor createSaleItemProcessor, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _createSaleItemProcessor = createSaleItemProcessor;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Sale details</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingSale = await _saleRepository.GetDraftByCustomerId(command.CustomerId, cancellationToken);
        if (existingSale != null)
            throw new InvalidOperationException($"A draft sale for customer {command.CustomerId} already exists");

        var sale = _mapper.Map<Sale>(command);
        var itemsResult = await CreateSaleItems(command.Items, cancellationToken);
        sale.Items = itemsResult.Select(i => i.Id).ToList();
        sale.TotalAmount = itemsResult.Sum(i => i.TotalAmount);

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }

    private async Task<IList<CreateSaleItemResult>> CreateSaleItems(IList<CreateSaleItem> request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<IEnumerable<CreateSaleItem>>(request);
        var tasks = command.Select(async i => await _createSaleItemProcessor.Process(i, cancellationToken));
        var results = await Task.WhenAll(tasks);
        return results;
    }
}
