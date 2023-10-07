using MoneyBankService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Domain.Entities
{
    public class Account : EntityBase
    {
        [Required(ErrorMessage = "El tipo de cuenta es obligatorio.")]
        [RegularExpression("^[AC]$", ErrorMessage = "El tipo de cuenta debe ser 'A' o 'C'.")]
        [StringLength(1, ErrorMessage = "La longitud máxima del tipo de cuenta es de 1 caracter.")]
        public char AccountType { get; set; } = 'A';

        [DataType(DataType.Date, ErrorMessage = "La fecha de creación debe ser una fecha válida.")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El número de cuenta es obligatorio.")]
        [MaxLength(10, ErrorMessage = "La longitud máxima del número de cuenta es de 10 caracteres.")]
        [RegularExpression(@"\d{10}", ErrorMessage = "El número de cuenta solo acepta números.")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "El nombre del propietario es obligatorio.")]
        [MaxLength(100, ErrorMessage = "La longitud máxima del nombre del propietario es de 100 caracteres.")]
        public string OwnerName { get; set; } = null!;

        [Required(ErrorMessage = "El monto de saldo es obligatorio.")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El monto de saldo debe estar en formato de dinero (0.00)")]
        public decimal BalanceAmount { get; set; }

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El monto de sobregiro debe estar en formato de dinero (0.00)")]
        public decimal OverdraftAmount { get; set; }

    }
}