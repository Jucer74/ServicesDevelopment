namespace MoneyBankService.Api.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
