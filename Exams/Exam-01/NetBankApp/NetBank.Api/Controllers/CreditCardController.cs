using Microsoft.AspNetCore.Mvc;
using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Infrastructure.Repositories;

namespace NetBank.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CreditCardController : ControllerBase
{
    // This class is a controller that handles requests for credit card data.

    private readonly ICreditCardService _creditCardService;
    private readonly IIssuingNetworkRepository _issuingNetworkRepository;

    // This constructor injects the dependencies for the controller.

    public CreditCardController(ICreditCardService creditCardService, IIssuingNetworkRepository issuingNetworkRepository)
    {
        _issuingNetworkRepository = issuingNetworkRepository;
        _creditCardService = creditCardService;
    }

    // This method gets the credit card data for the specified credit card number.

    [HttpGet("{creditcardNumber}")]
    public async Task<IActionResult> GetCreditCarDatad(string creditcardNumber)
    {
        // Get the list of issuing network data.
        List<IssuingNetworkData> issuingNetworkDataList = await this._creditCardService.LoadIssuingNetworkData();

        // Validate the credit card number.
        var validateResult = await this._creditCardService.Validate(creditcardNumber);

        // Get the result of the validation.
        var result = this._creditCardService.Result;

        // Switch on the validation result.
        switch (validateResult)
        {
            // The credit card number is valid.
            // The `Ok()` method returns a 200 OK response with the credit card data in the body.
            case ValidationResultType.Ok:
                return Ok(result);

            // The credit card number is invalid.
            // The `BadRequest()` method returns a 400 Bad Request response with the error message in the body.
            case ValidationResultType.BadRequest:
                return BadRequest(result);

            // The credit card number is not found.
            // The `NotFound()` method returns a 404 Not Found response.
            case ValidationResultType.NotFound:
                return NotFound(result);

            // An unknown error occurred.
            // The `StatusCode()` method returns a 500 Internal Server Error response with the error message in the body.
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, new CreditCardResult("Internal Server Error", false));
        }
    }
}
