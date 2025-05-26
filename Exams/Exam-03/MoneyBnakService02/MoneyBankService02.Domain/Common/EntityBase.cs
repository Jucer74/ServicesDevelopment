using System.ComponentModel.DataAnnotations;

namespace MoneyBankService02.Domain.Common;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}
