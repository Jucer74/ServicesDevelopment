using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pricat.Application.DTOs;
using Pricat.Application.Execptions;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using System.Net;
using AutoMapper;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }
            return Ok(_mapper.Map<ProductDto>(product));
        }
       
        // GET api/<ProductController>/5
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Products product)
        {
            if (product == null)
            {
                throw new BadRequestException("User is null");
            }
            var createdProduct = await _productService.AddAsync(product);

            return Ok(_mapper.Map<CrearProductDto>(createdProduct));
        }


        // GET api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Products product)
        {
            if (id != product.Id)
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{product.Id}]");

            product.Id = id;
            var updatedProduct = await _productService.UpdateAsync(id, product);
            return Ok(_mapper.Map<CrearProductDto>(updatedProduct));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Entity with Id={id} not found");
            }
            await _productService.DeleteAsync(id);
            return Ok();
        }

    }
}
