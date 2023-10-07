using MoneyBankService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Domain.Entities
{
    public class Account : EntityBase
    {
        [Required(ErrorMessage = "El tipo de cuenta es requerido")]
        [RegularExpression("^[AC]$", ErrorMessage = "El tipo de cuenta solo permite 'A' o 'C'")]
        [StringLength(1, ErrorMessage = "El tipo de cuenta es hasta 1 caracter")]
        public char AccountType { get; set; } = 'A';

        [DataType(DataType.Date, ErrorMessage = "La fecha de creacion es requerida")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El numero de cuenta es requerido")]
        [MaxLength(10, ErrorMessage = "El numero de cuenta es hasta 10 caracteres")]
        [RegularExpression(@"\d{10}", ErrorMessage = "El numero de cuenta solo permite los valores 0-9")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "El nombre del propietario es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre del propietario es hasta 100 caracteres")]
        public string OwnerName { get; set; } = null!;

        [Required(ErrorMessage = "El saldo es requerido")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El saldo debe estar en formato dinero (0,00)")]
        public decimal BalanceAmount { get; set; }

        [Required(ErrorMessage = "El monto de sobregiro es requerido")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El monto del sobregiro debe estar en formato de dinero (0,00)")]
        public decimal OverdraftAmount { get; set; }
    }
}