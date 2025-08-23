using System.ComponentModel.DataAnnotations;

namespace MoneyBankService02.Application.Dto;

public class AccountDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Debe especificar el tipo de cuenta")]
    [RegularExpression("[AC]", ErrorMessage = "Solo se permite A o C como tipo de cuenta")]
    public char AccountType { get; set; } = 'A';

    [DataType(DataType.Date)]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Se requiere el número de cuenta")]
    [MaxLength(10, ErrorMessage = "Máximo 10 caracteres")]
    [RegularExpression(@"\d{10}", ErrorMessage = "Debe contener exactamente 10 dígitos")]
    public string AccountNumber { get; set; } = null!;

    [Required(ErrorMessage = "El nombre del propietario es obligatorio")]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
    public string OwnerName { get; set; } = null!;

    [Required(ErrorMessage = "Debe especificar un balance")]
    [Range(0, double.MaxValue, ErrorMessage = "El saldo debe ser positivo")]
    public decimal BalanceAmount { get; set; }

    [Required(ErrorMessage = "Debe especificar el monto de sobregiro")]
    [Range(0, double.MaxValue, ErrorMessage = "El límite de sobregiro debe ser positivo")]
    public decimal OverdraftAmount { get; set; }
}
