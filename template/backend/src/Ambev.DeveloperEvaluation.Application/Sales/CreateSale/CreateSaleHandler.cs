using AutoMapper;
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

        var sale = _mapper.Map<Sale>(command);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        command.Items.Select(i => i.SaleId = createdSale.Id).ToList();

        var itemsResult = await CreateSaleItems(command.Items, cancellationToken);
        createdSale.TotalAmount = itemsResult.Sum(i => i.TotalAmount);
        await _saleRepository.UpdateAsync(createdSale, cancellationToken);

        var result = _mapper.Map<CreateSaleResult>(createdSale);
        result.Items = itemsResult.ToList();
        return result;
    }

    private async Task<IEnumerable<CreateSaleItemResult>> CreateSaleItems(IList<CreateSaleItem> command, CancellationToken cancellationToken)
    {
        var results = new CreateSaleItemResult[command.Count];
        for (int i = 0; i < command.Count; i++)
        {
            var item = command[i];
            results[i] = await _createSaleItemProcessor.Process(item, cancellationToken);
        }
        return results;
    }
}
