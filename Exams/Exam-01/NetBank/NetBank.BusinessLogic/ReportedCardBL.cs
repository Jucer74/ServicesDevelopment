using Microsoft.EntityFrameworkCore;
using NetBank.BusinessLogic;
using NetBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBank.DataAccess
{
    public class ReportedCardBL
    {
        private readonly ReportedCardDA _reportedCardDA;

        public ReportedCardBL(ReportedCardDA reportedCardDA)
        {
            _reportedCardDA = reportedCardDA;
        }

        public async Task<IList<ReportedCard>> GetAllReportedCards()
        {
            return await _reportedCardDA.GetAllReportedCards();

        }
        public async Task<IList<ReportedCard>> GetAllReportedCardsByIssuingNetworkName(string issuingIssuingNetworkName)
        {
            return await _reportedCardDA.GetAllReportedCardsByIssuingNetworkName(issuingIssuingNetworkName);

        }
        public async Task<ReportedCard> GetReportedCard(string CreditCardNumber)
        {
            return await _reportedCardDA.GetReportedCard(CreditCardNumber);

        }
        public async Task<string> PutCreditCardReactivated(string CreditCardNumber)
        {
            var reportedCard = await _reportedCardDA.PutCreditCardReactivated(CreditCardNumber);


            if (reportedCard is null)
            {
                return "Credit Card Not found";
            }

            reportedCard.StatusCard = "Recovered";
            reportedCard.LastUpdatedDate = DateTime.Now;
            await _reportedCardDA.SaveChangesAsync();

            return "Card Recovered";

        }

    }
}