using MediatR;
using Pricat.Domain.Entities;

namespace Pricat.Application.UseCases.GetProductById;

public class GetProductByIdQuery : IRequest<Product?>
{
    public int Id { get; set; }
}