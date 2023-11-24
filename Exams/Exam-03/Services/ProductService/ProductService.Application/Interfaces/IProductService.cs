using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductService.Application.Interfaces
{ 
   public interface IProductService
{


    Task<IEnumerable<Product>> GetAllProducts(); 
    Task<Product> GetProductById(int id);
     Task<Product> AddProduct(Product category);
    Task<Product> UpdateProduct(int id, Product category);
    Task RemoveProduct(int id);

    Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId);

        Task RemoveProductsByCategoryId(int categoryId);



        //Task<IEnumerable<CategoryProductsDto>> GetProductsByCategoryId(int productId);
    }
    
}
