using Microsoft.AspNetCore.Mvc;
using Arepas.Api.Middleware;
using Arepas.Domain.Exceptions;
using Arepas.Application.Interfaces;
using System.Net;
using Arepas.Domain.Entities.Models;
using Arepas.Domain.Entities.Dto;
using Arepas.Api.MiddleWare;

namespace Arepas.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Order>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Get(
             [FromQuery] PaginationParams paginationParams, string? q = null
             )
        {
            // Normal Execution
            if (paginationParams.Page == 0)
            {
                return Ok(await _orderService.GetAllAsync());
            }

            // Search
            if (q != null)
            {
                return Ok(await _orderService.SearchAsync(q));
            }
            var paginationResult = await _orderService.GetByPageAsync(paginationParams);

            Response.Headers.Add("X-Total-Count", paginationResult.XTotalCount.ToString());
            Response.Headers.Add("Link", paginationResult.Links);

            return Ok(paginationResult.Item);
            //            return Ok(await _productService.GetAllAsync());
        }
        [HttpGet("Customer/{id}")]
        public async Task<IActionResult> GetOrodersCustomers(
           int id)
        {
            var paginationResult = await _orderService.GetByCustomerAsync(id);

            Response.Headers.Add("X-Total-Count", paginationResult.XTotalCount.ToString());

            Response.Headers.Add("Link", paginationResult.Links);

            return Ok(paginationResult.Item);

        }
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _orderService.GetByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order category)
        {
            return Ok(await _orderService.AddAsync(category));
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Order order)
        {
            try
            {
                return Ok(await _orderService.UpdateAsync(id, order));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderService.RemoveAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}