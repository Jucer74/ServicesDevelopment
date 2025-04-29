using MediatR;
using Pricat.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pricat.Application.UseCases.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetByIdAsync(request.Id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with Id {request.Id} not found");
        }

        await _productRepository.RemoveAsync(request.Id);
    }
}