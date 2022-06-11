using Microsoft.AspNetCore.Mvc;
using Pricat.Domain.Entities;
using System.Threading.Tasks;
using Pricat.Utilities;
using Pricat.Application.interfaces;
using Application.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;
        private readonly ICategoriesService _categoryService;
        public ProductsController(IProductsService ProductsService)
        {
            _productService = ProductsService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (_productService.GetAll() != null)
            {
                return Ok(await _productService.GetAll());

            }
            else
            {
                throw new InternalServerErrorException("error");
            }
        }

        // GET api/v1/<ProductsController>/category/5
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            if (await CategoryExist(id) != false)
            {
                return Ok(await _productService.GetAllBycategoryId(id));
            }
            else
            {
                throw new NotFoundException("error");
            }
            
        }
        private async Task<bool> CategoryExist(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _productService.GetById(id);

            try
            {
                if (category != null)
                {
                    return Ok(category);

                }
                else
                {
                    throw new BusinessException("error");
                }
            }
            catch
            {
                throw new InternalServerErrorException("error");
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> add([FromBody] Products product)
        {

            if (Ean13Calculator.IsValid(product.EanCode))
            {
                await _productService.Add(product);
                    return Ok();
            }
            throw new BusinessException("error");

        }

        // PUT api/v1/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Products product)
        {
            if (id != product.Id)
            {
                throw new BusinessException("error");
            }

            if (Ean13Calculator.IsValid(product.EanCode))
            {
                await _productService.Update(product);
                return Ok();
            }
            else
            {
                throw new BusinessException("error");
            }
        }

        // DELETE api/v1/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            if (await ProductExist(id) != false)
            {
                await _productService.Remove(id);
                return Ok();
            }
            else
            {
                throw new NotFoundException("error");
            }
        }

        private async Task<bool> ProductExist(int id)
        {
            var category = await _productService.GetById(id);
            if (category != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
