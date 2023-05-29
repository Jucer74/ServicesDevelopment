using Arepas.Api.Dtos;
using Arepas.Application.Interfaces;
using Arepas.Domain.Dtos;
using Arepas.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Arepas.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(IMapper mapper, ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParams queryParams)
        {
            if (queryParams.Page == 0 && queryParams.Limit == 0)
            {
                return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(await _customerService.GetAllAsync()));
            }

            var responseData = await _customerService.GetByQueryParamsAsync(queryParams);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(responseData.XPagination));

            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(responseData.Items));
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<CustomerDto>(await _customerService.GetByIdAsync(id)));
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            return Ok(_mapper.Map<CustomerDto>(await _customerService.AddAsync(_mapper.Map<Customer>(customerDto))));
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerDto customerDto)
        {
            return Ok(_mapper.Map<CustomerDto>(await _customerService.UpdateAsync(id, _mapper.Map<Customer>(customerDto))));
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.RemoveAsync(id);
            return Ok();
        }
    }
}