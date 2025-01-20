using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICreateSaleItemProcessor _createSaleItemProcessor;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _createSaleItemProcessor = Substitute.For<ICreateSaleItemProcessor>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _createSaleItemProcessor, _mapper);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            CustomerId = command.CustomerId,
            BranchId = command.BranchId
        };

        var result = new CreateSaleResult
        {
            Id = sale.Id,
        };


        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);
        for (var i = 0; i < command.Items.Count; i++)
        {
            var c = command.Items[i];
            _mapper.Map<CreateSaleItemResult>(c).Returns(new CreateSaleItemResult
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity
            });
        }

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);
        _createSaleItemProcessor.Process(Arg.Any<CreateSaleItem>(), Arg.Any<CancellationToken>())
            .Returns(c => _mapper.Map<CreateSaleItemResult>(c.Arg<CreateSaleItem>()));

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to sale entity")]
    public async Task Handle_ValidRequest_MapsCommandToSale()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            CustomerId = command.CustomerId,
            BranchId = command.BranchId
        };
        var createSale = new CreateSaleResult
        {
            Id = sale.Id,
        };

        _mapper.Map<CreateSaleResult>(sale).Returns(createSale);
        _mapper.Map<Sale>(command).Returns(sale);
        for (var i = 0; i < command.Items.Count; i++)
        {
            var c = command.Items[i];
            _mapper.Map<CreateSaleItemResult>(c).Returns(new CreateSaleItemResult
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity
            });
        }

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);
        _createSaleItemProcessor.Process(Arg.Any<CreateSaleItem>(), Arg.Any<CancellationToken>())
            .Returns(c => _mapper.Map<CreateSaleItemResult>(c.Arg<CreateSaleItem>()));

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
            c.CustomerId == command.CustomerId &&
            c.BranchId == command.BranchId &&
            CompareItems(c.Items, command.Items)));
    }

    private bool CompareItems(IList<CreateSaleItem> items, IList<CreateSaleItem> items1)
    {
        return items.Count == items1.Count && items.Select((item, i) =>
        {
            var item1 = items1[i];
            return item.ProductId == item1.ProductId && item.Quantity == item1.Quantity ? 1 : 0;
        }).Count() == items1.Count;
    }
}
