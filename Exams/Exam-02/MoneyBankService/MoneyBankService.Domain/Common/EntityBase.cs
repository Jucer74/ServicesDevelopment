using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Domain.Common;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}