<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using NetBank.Application.Interfaces;
=======
﻿using Microsoft.AspNetCore.Mvc;
using Netbank.Application.Interfaces;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
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
<<<<<<< HEAD
    public async Task<IActionResult> GetCreditCarData(string creditcardNumber)
=======
    public async Task<IActionResult> GetCreditCarDatad(string creditcardNumber)
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
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