using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBankService.Application.DTO
{
   public class TransactionCreateDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public decimal ValueAmount { get; set; }
    }
}
