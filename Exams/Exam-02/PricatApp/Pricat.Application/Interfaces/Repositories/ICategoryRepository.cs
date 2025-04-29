using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetCategoryByIdIncludeProduct(int id);
    }
}