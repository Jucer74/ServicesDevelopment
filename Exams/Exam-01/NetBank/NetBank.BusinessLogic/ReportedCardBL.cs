using NetBank.DataAccess;
using NetBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBank.BusinessLogic
{
    public class ReportedCardBL
    {
        private readonly ReportedCardDA _ReportedCardDA;
        public ReportedCardBL(ReportedCardDA reportedCard)
        {
            this._ReportedCardDA = reportedCard;
        }

        public async Task<List<ReportedCard>> GetAllReportedCards()
        {
            return (List<ReportedCard>)await _ReportedCardDA.GetAllReportedCards();
        }

        public async Task<List<ReportedCard>> GetAllReportedCardsByIssuingNetworkName(string issuingNetwork)
        {
            return await _ReportedCardDA.GetAllReportedCardsByIssuingNetworkName(issuingNetwork);
        }

        public async Task<List<ReportedCard>> GetReportedCard(string creditCardNumber)
        {
            return await _ReportedCardDA.GetReportedCard(creditCardNumber);
        }

        public async Task<string> PutCreditCardReactivated(string creditCardNumber)
        {
            return await _ReportedCardDA.PutCreditCardReactivated(creditCardNumber);
        }
    }
}