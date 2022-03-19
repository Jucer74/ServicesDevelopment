using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBank.DataAccess;
using NetBank.Models;

namespace NetBank.BusinessLogic
{
    public class ReportedCardBL
    {
        private readonly ReportedCardDA _reportedCardDA;

        public ReportedCardBL(ReportedCardDA reportedCardDA)
        {
            _reportedCardDA = reportedCardDA;
        }
        public async Task<List<ReportedCard>> GetAllReportedCards()
        {
            return await _reportedCardDA.GetAllReportedCards();
        }

        public async Task<List<ReportedCard>> GetAllReportedCardsByIssuingNetworkName(string issuingNetwork)
        {
            return await _reportedCardDA.GetAllReportedCardsByIssuingNetworkName(issuingNetwork);
        }

        public async Task<List<ReportedCard>> GetReportedCard(string CreditCardNumber)
        {
            return await _reportedCardDA.GetReportedCard(CreditCardNumber);
        }

        public async Task<String> PutCreditCardReactivated(string CreditCardNumber)
        {
            return await _reportedCardDA.PutCreditCardReactivated(CreditCardNumber);
        }

    }
}
