using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBankService.Application.DTO
{
    public class AccountResultDto
    {
        public int Id { get; set; }
        public char AccountType { get; set; }
        public DateTime CreationDate { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public decimal BalanceAmount { get; set; }
        public decimal OverdraftAmount { get; set; }
    }
}
