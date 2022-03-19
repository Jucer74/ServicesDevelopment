using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBank.BusinessLogic;
using NetBank.BussisnesLogic;
using NetBank.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetBank.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ReportedCardsController : Controller
    {
        private readonly ReportedCardBL _reportedCardBL;
        public ReportedCardsController(ReportedCardBL reportedCardBL)
        {
            _reportedCardBL = reportedCardBL;
        }

        [HttpGet]
        public async Task<ActionResult<IList<ReportedCard>>> GetAllReportedCards()
        {
            return Ok(await _reportedCardBL.GetAllReportedCards());
        }


        [HttpGet("IssuingNetwork/{issuingNetworkName}}")]
        public async Task<ActionResult<IList<ReportedCard>>> GetAllReportedCardsByIssuingNetworkName(string issuingNetwork)
        {
            return Ok(await _reportedCardBL.GetAllReportedCardsByIssuingNetworkName(issuingNetwork));
        }


        [HttpGet("{creditCardNumber}")]
        public async Task<ActionResult<IList<ReportedCard>>> GetReportedCard(string creditCardNumber)
        {
            return Ok(await _reportedCardBL.GetReportedCard(creditCardNumber));
        }




        [HttpGet("{creditCardNumber}")]
        public async Task<ActionResult<string>> PutCreditCardReactivated(string creditCardNumber)
        {
            return Ok(await _reportedCardBL.PutCreditCardReactivated(creditCardNumber));
        }


        [HttpPost("{creditCardNumber}")]
        public ActionResult<string> PostCheckCreditCardDigit(string CreditCardNumber)
        {
            if (CreditCardBL.IsValid(CreditCardNumber))
            {
                return Ok("Credit Card is Valid");
            }

            return Ok("Credit Card is NOT Valid");
        }


        // GET: ReportedCardsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReportedCardsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReportedCardsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportedCardsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportedCardsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReportedCardsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportedCardsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReportedCardsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}