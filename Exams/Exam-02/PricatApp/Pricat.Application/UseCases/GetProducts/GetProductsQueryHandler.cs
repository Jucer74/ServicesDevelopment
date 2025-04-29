using MediatR;
using Pricat.Domain.Interfaces;
using Pricat.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pricat.Application.UseCases.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync();
    }
}