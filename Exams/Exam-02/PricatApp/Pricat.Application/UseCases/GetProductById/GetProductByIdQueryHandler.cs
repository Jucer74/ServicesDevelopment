using MediatR;
using Pricat.Domain.Interfaces;
using Pricat.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Pricat.Application.UseCases.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(request.Id);
    }
}