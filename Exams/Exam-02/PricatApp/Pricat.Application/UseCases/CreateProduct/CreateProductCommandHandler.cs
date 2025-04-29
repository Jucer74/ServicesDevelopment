using MediatR;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pricat.Application.UseCases.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Verificar que la categoría exista
        var category = await _categoryRepository.GetByIdAsync(request.Product.CategoryId);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with Id {request.Product.CategoryId} not found");
        }

        // Validar EAN Code único
        var existingProduct = (await _productRepository.GetAllAsync())
            .FirstOrDefault(p => p.EanCode == request.Product.EanCode);

        if (existingProduct != null)
        {
            throw new ArgumentException($"Product with EAN Code {request.Product.EanCode} already exists");
        }

        return await _productRepository.AddAsync(request.Product);
    }
}