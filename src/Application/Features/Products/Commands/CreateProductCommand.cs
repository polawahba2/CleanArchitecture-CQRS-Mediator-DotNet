using MediatR;
namespace Application.Features.Products.Commands
{
    public record CreateProductCommand(
      string Name,
      string Description,
      decimal Price,
      int StockQuantity) : IRequest<Guid>;
}