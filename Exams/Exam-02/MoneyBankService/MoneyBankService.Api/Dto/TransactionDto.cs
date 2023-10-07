namespace MoneyBankService.Api.Dto;

public class TransactionDto
{
    
        public int Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string  ValueAmount { get; set; } = null!;
}


