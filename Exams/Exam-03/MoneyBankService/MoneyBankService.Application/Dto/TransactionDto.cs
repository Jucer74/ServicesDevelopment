using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyBankService.Application.Dto;


namespace MoneyBankService.Application.Dto;
public class TransactionDto
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal ValueAmount { get; set; }
}