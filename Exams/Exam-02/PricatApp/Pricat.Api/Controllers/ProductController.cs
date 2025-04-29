using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Interfaces;
using Pricat.Application.Exceptions;
using Pricat.Application.DTOs;
using System.Linq;
using System.Threading.Tasks;
using Pricat.Domain.Common;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
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

        // GET api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                if (products == null || !products.Any())
                {
                    throw new NotFoundException("No products found.");
                }

                // Mapeamos las entidades a DTOs
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(productDtos);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException("An unexpected error occurred.", ex);
            }
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {id} not found.");
                }

                // Mapeamos la entidad a DTO
                var productDto = _mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException("An unexpected error occurred.", ex);
            }
        }

        /// POST api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
        {
            try
            {
                if (productCreateDto == null)
                {
                    throw new BadRequestException("Product data is invalid.");
                }

                // Mapeamos el DTO a entidad
                var product = _mapper.Map<Product>(productCreateDto);
                await _productService.AddProductAsync(product);

                // Mapeamos la entidad a DTO para la respuesta
                var productDto = _mapper.Map<ProductDto>(product);
                return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
            }
            catch (BadRequestException ex)
            {
                throw new BadRequestException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException("An unexpected error occurred.", ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateDto productCreateDto)
        {
            try
            {
                if (id != productCreateDto.Id)
                {
                    throw new BadRequestException("Product ID mismatch.");
                }

                // Verificamos si el producto existe en la base de datos
                var existingProduct = await _productService.GetProductByIdAsync(id);
                if (existingProduct == null)
                {
                    throw new NotFoundException($"Product with ID {id} not found.");
                }

                // Mapeamos el DTO a la entidad para la actualización
                var product = _mapper.Map<Product>(productCreateDto);

                // Llamamos al servicio para actualizar el producto
                await _productService.UpdateProductAsync(product);

                // Retornamos NoContent ya que la actualización fue exitosa
                return NoContent();
            }
            catch (BadRequestException ex)
            {
                throw new BadRequestException(ex.Message, ex);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException("An unexpected error occurred.", ex);
            }
        }


        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {id} not found.");
                }

                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException("An unexpected error occurred.", ex);
            }
        }
    }
}
