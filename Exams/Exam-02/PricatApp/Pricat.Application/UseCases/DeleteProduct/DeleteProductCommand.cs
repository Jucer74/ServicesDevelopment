using MediatR;

namespace Pricat.Application.UseCases.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public int Id { get; set; }
}