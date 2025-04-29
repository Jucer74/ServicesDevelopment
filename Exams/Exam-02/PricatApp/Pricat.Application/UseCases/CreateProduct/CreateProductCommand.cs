using MediatR;
using Pricat.Domain.Entities;

namespace Pricat.Application.UseCases.CreateProduct;

public class CreateProductCommand : IRequest<Product>
{
    public Product Product { get; set; } = null!;
}