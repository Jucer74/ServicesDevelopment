using Arepas.Api.MiddleWare;
using Arepas.Application.Interfaces;
using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;
using Arepas.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Arepas.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrdersDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        // GET: api/<OrderDetailController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<OrderDetail>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
        {
            // Normal Execution
            if (paginationParams.Page == 0)
            {
                return Ok(await _orderDetailService.GetAllAsync());
            }


            var paginationResult = await _orderDetailService.GetByPageAsync(paginationParams);

            Response.Headers.Add("X-Total-Count", paginationResult.XTotalCount.ToString());
            Response.Headers.Add("Link", paginationResult.Links);

            return Ok(paginationResult.Item);
            //            return Ok(await _productService.GetAllAsync());
        }

        //GET: api/OrderDetailss/Order
        [HttpGet("Order/{id}")]
        public async Task<IActionResult> GetOrodersCustomers(int id)
        {
            var paginationResult = await _orderDetailService.GetByOrderAsync(id);

            Response.Headers.Add("X-Total-Count", paginationResult.XTotalCount.ToString());

            Response.Headers.Add("Link", paginationResult.Links);

            return Ok(paginationResult.Item);

        }

        // GET api/<OrderDetailssController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _orderDetailService.GetByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<OrderDetailssController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDetail category)
        {
            try
            {
                return Ok(await _orderDetailService.AddOrderDetail(category));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        // PUT api/<OrderDetailsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderDetail orderDetail)
        {
            try
            {
                return Ok(await _orderDetailService.UpdateAsync(id, orderDetail));
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

        // DELETE api/<OrderDetailsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderDetailService.RemoveAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}