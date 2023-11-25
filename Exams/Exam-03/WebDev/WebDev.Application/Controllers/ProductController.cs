using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDev.Application.Config;
using WebDev.Application.Models;
using WebDev.Services;
using WebDev.Services.Entities;

namespace WebDev.Application.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiConfiguration _apiConfiguration;
        private ProductService librosService;

        public ProductsController(IOptions<ApiConfiguration> apiConfiguration)
        {
            _apiConfiguration = apiConfiguration.Value;
            librosService = new ProductsService(_apiConfiguration.ApiProductsUrl);
        }

        // GET: ProductsController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IList<ProductDto> libros = await librosService.GetProducts();

            return View(libros.Select(libroDto => MapperToProduct(libroDto)).ToList());
        }

        // GET: ProductsController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var libroFound = await librosService.GetProductById(id);

            if (libroFound == null)
            {
                return NotFound();
            }

            return View(MapperToProduct(libroFound));
        }

        // GET: ProductsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Producto libro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var libroDto = MapperToProductDto(libro);
                    await librosService.AddProduct(libroDto);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var libroFound = await librosService.GetProductById(id);

            if (libroFound == null)
            {
                return NotFound();
            }

            return View(MapperToProduct(libroFound));
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Producto libro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var libroDto = MapperToProductDto(libro);
                    await librosService.UpdateProduct(libroDto);
                    return RedirectToAction(nameof(Index));
                }
                return View(libro);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var libroFound = await librosService.GetProductById(id);

            if (libroFound == null)
            {
                return NotFound();
            }

            return View(MapperToProduct(libroFound));
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Producto libro)
        {
            try
            {
                var libroFound = await librosService.GetProductById(libro.Id);

                if (libroFound == null)
                {
                    return View();
                }

                await librosService.DeleteProduct(libro.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Producto MapperToProduct(ProductDto libroDto)
        {
            return new Producto
            {
                Id = libroDto.Id,
                Titulo = libroDto.Titulo,
                Autor = libroDto.Autor,
                Precio = libroDto.Precio,
                Cantidad = libroDto.Cantidad,
                Imagen = libroDto.Imagen
            };
        }

        private ProductDto MapperToProductDto(Producto libro)
        {
            return ProductDto.Build(
                id: libro.Id,
                titulo: libro.Titulo,
                autor: libro.Autor,
                precio: libro.Precio,
                cantidad: libro.Cantidad,
                imagen: libro.Imagen
            );
        }
    }
}
