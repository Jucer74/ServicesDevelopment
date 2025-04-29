using MediatR;
using Pricat.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pricat.Application.UseCases.GetProductsByCategoryId;

public class GetProductsByCategoryIdQueryHandler : IRequestHandler<GetProductsByCategoryIdQuery, List<Product>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public GetProductsByCategoryIdQueryHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Product>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        // Verificar si la categoría existe
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with Id {request.CategoryId} not found");
        }

        return await _productRepository.GetByCategoryIdAsync(request.CategoryId);
    }
}