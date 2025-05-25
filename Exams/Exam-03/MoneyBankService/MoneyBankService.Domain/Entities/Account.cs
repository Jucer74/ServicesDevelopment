using System.ComponentModel.DataAnnotations;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Entities;

public class Account : EntityBase
{
    [Required]
    [RegularExpression("[AC]")]
    public char AccountType { get; set; } = 'A';

    [DataType(DataType.DateTime)]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(10)]
    [RegularExpression(@"^\d{10}$")]
    public string AccountNumber { get; set; } = null!; // null! indica que no será null en tiempo de ejecución.

    [Required]
    [StringLength(100)] // Paso 5: StringLength para longitud máxima de 100.
    public string OwnerName { get; set; } = null!;

    [Required]
    // Paso 6: Expresión regular para formato moneda (hasta 2 decimales).
    // Puedes considerar agregar [Range] para asegurar que no sea negativo si esa es una regla del dominio.
    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    public decimal BalanceAmount { get; set; }

    [Required]
    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    public decimal OverdraftAmount { get; set; }
}