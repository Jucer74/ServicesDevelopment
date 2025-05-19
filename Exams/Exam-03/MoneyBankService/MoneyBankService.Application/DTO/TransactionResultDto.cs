using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBankService.Application.DTO
{
    public class TransactionResultDto
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public decimal ValueAmount { get; set; }
    }
}
