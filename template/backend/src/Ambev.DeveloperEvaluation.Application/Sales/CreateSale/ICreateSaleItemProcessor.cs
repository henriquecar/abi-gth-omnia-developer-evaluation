
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public interface ICreateSaleItemProcessor
    {
        Task<CreateSaleItemResult> Process(CreateSaleItem command, CancellationToken cancellationToken);
    }
}