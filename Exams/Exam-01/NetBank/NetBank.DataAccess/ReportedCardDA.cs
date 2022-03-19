using Microsoft.EntityFrameworkCore;
using NetBank.DataAccess.Context;
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

        public async Task<List<ReportedCard>> GetAllReportedCards()
        {
            return await _context.ReportedCards.ToListAsync();
        }

        public async Task<List<ReportedCard>> GetAllReportedCardsByIssuingNetworkName(string issuingNetwork)
        {
            return await _context.ReportedCards.Where(e => e.IssuingNetwork == issuingNetwork).ToListAsync();
        }

        public async Task<List<ReportedCard>> GetReportedCard(string CreditCardNumber)
        {
            return await _context.ReportedCards.Where(p => p.CreditCardNumber == CreditCardNumber).ToListAsync();
        }

        public async Task<String> PutCreditCardReactivated(string CreditCardNumber)
        {
            var reportedCard = _context.ReportedCards.FirstOrDefault(p => p.CreditCardNumber == CreditCardNumber);

            if (reportedCard != null)
            {
                return "Credit Card Not found";
            }
            else
            {
                reportedCard.StatusCard = "Recovered";
                reportedCard.LastUpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return "Credit Card Recovered";
            }
        }
    }
}