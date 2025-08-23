using System.ComponentModel.DataAnnotations;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Entities
{
    public class BankAccount : EntityBase
    {
        [Required(ErrorMessage = "El Tipo de Cuenta es obligatorio")]
        [RegularExpression("[AC]", ErrorMessage = "Solo se permiten los valores 'A' o 'C'")]
        public char AccountType { get; set; } = 'A';

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El Número de Cuenta es obligatorio")]
        [MaxLength(10, ErrorMessage = "El Número de Cuenta no puede exceder 10 caracteres")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Debe contener exactamente 10 dígitos")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "El Nombre del Titular es obligatorio")]
        [MaxLength(100, ErrorMessage = "El Nombre del Titular no puede exceder 100 caracteres")]
        public string OwnerName { get; set; } = null!;

        [Required(ErrorMessage = "El Saldo es obligatorio")]
        [RegularExpression(@"^\d+\.?\d{0,2}$", ErrorMessage = "Formato inválido (ejemplo válido: 100.50)")]
        public decimal BalanceAmount { get; set; }

        [Required(ErrorMessage = "El Límite de Sobregiro es obligatorio")]
        [RegularExpression(@"^\d+\.?\d{0,2}$", ErrorMessage = "Formato inválido (ejemplo válido: 500.00)")]
        public decimal OverdraftAmount { get; set; }
    }
}