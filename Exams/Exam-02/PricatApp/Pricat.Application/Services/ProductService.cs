using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;
namespace Pricat.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _teamMemberRepository;

    public ProductService(IProductRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }
    public async Task<Product> CreateProduct(Product teamMember)
    {
        return await _teamMemberRepository.AddAsync(teamMember);
    }

    public async Task DeleteProduct(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);

        if (teamMember == null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        await _teamMemberRepository.RemoveAsync(teamMember);
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return (await _teamMemberRepository.GetAllAsync()).ToList();
    }

    public async Task<Product> GetProductById(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);

        if (teamMember == null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        return teamMember;
    }

    public async Task<Product> UpdateProduct(int id, Product entity)
    {
        if (id != entity.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Product.Id [{entity.Id}]");
        }

        var teamMember = await _teamMemberRepository.GetByIdAsync(id);
        if (teamMember is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }
        return await _teamMemberRepository.UpdateAsync(entity);
    }
}