using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetProductsQuery : IRequest<IEnumerable<Product>>;

}