using Microsoft.AspNetCore.Mvc;
using NetBank.BusinessLogic;
using NetBank.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NetBank.Api.Controller
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ReportedCardsController : ControllerBase
    {
        private readonly ReportedCardBL _reportedCardBL;

        public ReportedCardsController(ReportedCardBL reportedCardBL)
        {
            _reportedCardBL = reportedCardBL;
        }

        // GET: api/v1.0/<ReportedCardsController>
        [HttpGet]
        public async Task<ActionResult<IList<ReportedCard>>> GetAllReportedCards()
        {
            return Ok(await _reportedCardBL.GetAllReportedCards());
        }

        // GET: api/v1.0/<ReportedCardsController>
        [HttpGet("IssuingNetwork/{issuingNetworkName}")]
        public async Task<ActionResult<IList<ReportedCard>>> GetAllReportedCardsByIssuingNetworkName(string issuingNetworkName)
        {
            return Ok(await _reportedCardBL.GetAllReportedCardsByIssuingNetworkName(issuingNetworkName));
        }

        // GET: api/v1.0/<ReportedCardsController>
        [HttpGet("{creditCardNumber}")]
        public async Task<ActionResult<IList<ReportedCard>>> GetReportedCard(string CreditCardNumber)
        {
            return Ok(await _reportedCardBL.GetReportedCard(CreditCardNumber));
        }

        [HttpPut("{creditCardNumber}")]
        public async Task<ActionResult<String>> PutCreditCardReactivated(string CreditCardNumber)
        {
            return Ok(await _reportedCardBL.PutCreditCardReactivated(CreditCardNumber));
        }

        [HttpPost("{creditCardNumber}")]
        public ActionResult<String> PostCheckCreditCardDigit(string CreditCardNumber)
        {
            if (CreditCardBL.IsValid(CreditCardNumber))
            {
                return Ok("Credit Card is Valid");
            }

            return Ok("Credit Card is NOT Valid");
        }
    }
}
