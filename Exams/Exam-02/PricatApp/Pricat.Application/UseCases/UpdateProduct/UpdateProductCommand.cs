using MediatR;
using Pricat.Domain.Entities;

namespace Pricat.Application.UseCases.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public int Id { get; set; }
    public Product Product { get; set; } = null!;
}