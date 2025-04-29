using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Pricat.Application.DTOs;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using Pricat.Domain.Repositories;
using Pricat.Utilities;

namespace Pricat.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository productRepo, ICategoryRepository categoryRepo)
    {
        _productRepository = productRepo;
        _categoryRepository = categoryRepo;
    }

    public async Task<List<ProductDto>> GetAllProducts()
    {
        var list = await _productRepository.GetAllAsync();
        return list.Select(ToDto).ToList();
    }

    public async Task<ProductDto?> GetProductById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : ToDto(product);
    }

    public async Task<List<ProductDto>> GetByCategoryId(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null)
            throw new KeyNotFoundException("Category not found");

        var list = await _productRepository.GetByCategoryIdAsync(categoryId);
        return list.Select(ToDto).ToList();
    }

    public async Task<ProductDto> CreateProduct(ProductDto dto)
    {
        ValidateEAN(dto.EAN13);

        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
        if (category == null)
            throw new KeyNotFoundException("Category not found");

        var entity = ToEntity(dto);
        var result = await _productRepository.AddAsync(entity);
        return ToDto(result);
    }

    public async Task UpdateProduct(int id, ProductDto dto)
    {
        var existing = await _productRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException("Product not found");

        ValidateEAN(dto.EAN13);
        existing.Name = dto.Name;
        existing.Price = dto.Price;
        existing.EAN13 = dto.EAN13;
        existing.Unit = dto.Unit;
        existing.CategoryId = dto.CategoryId;

        await _productRepository.UpdateAsync(existing);
    }

    public async Task DeleteProduct(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    private static ProductDto ToDto(Product product) => new()
    {
        Id = product.Id,
        CategoryId = product.CategoryId,
        EAN13 = product.EAN13,
        Name = product.Name,
        Unit = product.Unit,
        Price = product.Price
    };

    private static Product ToEntity(ProductDto dto) => new()
    {
        Id = dto.Id,
        CategoryId = dto.CategoryId,
        EAN13 = dto.EAN13,
        Name = dto.Name,
        Unit = dto.Unit,
        Price = dto.Price
    };

    private void ValidateEAN(string ean)
    {
        if (!Ean13Calculator.IsValid(ean))
            throw new ArgumentException("EAN-13 is not valid");
    }
}
