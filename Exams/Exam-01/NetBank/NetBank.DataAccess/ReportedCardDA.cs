using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream
using Netbank.Api.Context;
using Netbank.DataAccess;
=======
using NetBank.DataAccess;
>>>>>>> Stashed changes
using NetBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< Updated upstream
=======
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
>>>>>>> Stashed changes


namespace NetBank.DataAccess
{
    public class ReportedCardDA
    {
<<<<<<< Updated upstream
        private readonly AppDbContext _context;
        public ReportedCardDA(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ReportedCard>> GetAllReportedCards()
        {
            return await _context.ReportedCards.ToListAsync();
=======
        private readonly AppDbContext _DBcontext;

        public ReportedCardDA(AppDbContext context)
        {
            _DBcontext = context;
        }

        public async Task<List<ReportedCard>> GetAllReportedCards()
        {
            return await _DBcontext.ReportedCards.ToListAsync();
>>>>>>> Stashed changes
        }

        public async Task<List<ReportedCard>> GetAllReportedCardsByIssuingNetworkName(string issuingNetwork)
        {
<<<<<<< Updated upstream
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

=======
            return await _DBcontext.ReportedCards.Where(e => e.IssuingNetwork == issuingNetwork).ToListAsync();
        }

        public async Task<List<ReportedCard>> GetReportedCard(string CreditCardNumber)
        {
            return await _DBcontext.ReportedCards.Where(a => a.CreditCardNumber == CreditCardNumber).ToListAsync();
        }

        public async Task<String> PutCreditCardReactivated(string CreditCardNumber)
        {
            var reportedCard = _DBcontext.ReportedCards.FirstOrDefault(r => r.CreditCardNumber == CreditCardNumber);

            if (reportedCard != null)
            {
                return "Tarjeta de Credito No Encontrada";
            }
            else
            {
                reportedCard.StatusCard = "Recuperada";
                reportedCard.LastUpdatedDate = DateTime.Now;
                await _DBcontext.SaveChangesAsync();
                return "Tarjeta de credito recuperada";
            }
        }
>>>>>>> Stashed changes
    }
}