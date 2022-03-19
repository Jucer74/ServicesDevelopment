using Microsoft.EntityFrameworkCore;
using Netbank.Api.Context;
using Netbank.DataAccess;
using NetBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NetBank.DataAccess
{
    public class ReportedCardDA
    {
        private readonly AppDbContext _context;
        public ReportedCardDA(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ReportedCard>> GetAllReportedCards()
        {
            return await _context.ReportedCards.ToListAsync();
        }

        public async Task<List<ReportedCard>> GetAllReportedCardsByIssuingNetworkName(string issuingNetwork)
        {
            return await _context.ReportedCards.Where(e => e.IssuingNetwork == issuingNetwork).ToListAsync();
        }

        public async Task<List<ReportedCard>> GetReportedCard(string creditCardNumber)
        {
            return await _context.ReportedCards.Where(a => a.CreditCardNumber == creditCardNumber).ToListAsync();
        }

        public async Task<string> PutCreditCardReactivated(string creditCardNumber)
        {
            var reportedCard = _context.ReportedCards.FirstOrDefault(e => e.CreditCardNumber == creditCardNumber);

            if (reportedCard is null)
            {
                return "error no found";
            }
            else
            {
                reportedCard.StatusCard = "Recovered";
                _context.ReportedCards.FirstOrDefault(e => e.CreditCardNumber == creditCardNumber).LastUpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return "Recovered";
            }
        }

    }
}