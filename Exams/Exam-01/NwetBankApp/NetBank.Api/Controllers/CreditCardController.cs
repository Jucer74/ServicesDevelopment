using Microsoft.AspNetCore.Mvc;
using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;

namespace NetBank.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CreditCardController : ControllerBase
{
    private readonly ICreditCardService _creditCardService;

    public CreditCardController(ICreditCardService creditCardService)
    {
        _creditCardService = creditCardService;
    }

    [HttpGet("{creditcardNumber}")]
    public async Task<IActionResult> GetCreditCarDatad(string creditcardNumber)
    {
        var validateResult = await _creditCardService.Validate(creditcardNumber);
        var result = _creditCardService.Result;

        switch (validateResult)
        {
            case ValidationResultType.Ok:
                return Ok(result);

            case ValidationResultType.BadRequest:
                return BadRequest(result);

            case ValidationResultType.NotFound:
                return NotFound(result);

            default:
                return StatusCode(StatusCodes.Status500InternalServerError, new CreditCardResult("Internal Server Error", false));
        }
    }
}