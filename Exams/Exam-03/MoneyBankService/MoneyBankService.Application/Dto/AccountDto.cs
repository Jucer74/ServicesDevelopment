using System;

namespace MoneyBankService.Application.Dto // Namespace actualizado
{
    public class AccountDto
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