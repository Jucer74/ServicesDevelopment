using MediatR;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pricat.Application.UseCases.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Verificar que la categoría exista
        var category = await _categoryRepository.GetByIdAsync(request.Product.CategoryId);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with Id {request.Product.CategoryId} not found");
        }

        // Verificar que el producto exista
        var existingProduct = await _productRepository.GetByIdAsync(request.Id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with Id {request.Id} not found");
        }

        // Validar EAN Code único (excepto para el producto actual)
        var products = await _productRepository.GetAllAsync();
        var productWithSameEan = products.FirstOrDefault(p =>
            p.EanCode == request.Product.EanCode && p.Id != request.Id);

        if (productWithSameEan != null)
        {
            throw new ArgumentException($"Product with EAN Code {request.Product.EanCode} already exists");
        }

        await _productRepository.UpdateAsync(request.Id, request.Product);
    }
}