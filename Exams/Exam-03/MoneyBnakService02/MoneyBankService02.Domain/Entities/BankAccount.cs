using System.ComponentModel.DataAnnotations;
using MoneyBankService02.Domain.Common;

namespace MoneyBankService02.Domain.Entities;

public class BankAccount : EntityBase
{
    [Required(ErrorMessage = "El Tipo de Cuenta es obligatorio")]
    [RegularExpression("[AC]", ErrorMessage = "Solo se permiten los valores 'A' o 'C'")]
    public char AccountType { get; set; } = 'A'; // Coincide con AccountType en SQL

    [DataType(DataType.Date)]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow; // Coincide con CreationDate en SQL

    [Required(ErrorMessage = "El Número de Cuenta es obligatorio")]
    [MaxLength(10, ErrorMessage = "Máximo 10 caracteres")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Debe contener exactamente 10 dígitos")]
    public string AccountNumber { get; set; } = null!; // Coincide con AccountNumber en SQL

    [Required(ErrorMessage = "El Nombre del Titular es obligatorio")]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
    public string OwnerName { get; set; } = null!; // Coincide con OwnerName en SQL

    [Required(ErrorMessage = "El Saldo es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El saldo debe ser positivo")]
    public decimal BalanceAmount { get; set; } // Coincide con BalanceAmount en SQL

    [Required(ErrorMessage = "El Límite de Sobregiro es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El límite debe ser positivo")]
    public decimal OverdraftAmount { get; set; } // Coincide con OverdraftAmount en SQL
}
