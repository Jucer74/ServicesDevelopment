using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Application.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe especificar el tipo de cuenta")]
        [RegularExpression("[AC]", ErrorMessage = "Solo se permite A o C como tipo de cuenta")]
        public char AccountType { get; set; } = 'A';

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Se requiere el número de cuenta")]
        [MaxLength(10, ErrorMessage = "Máximo 10 caracteres para el número de cuenta")]
        [RegularExpression(@"\d{10}", ErrorMessage = "El número de cuenta debe contener exactamente 10 dígitos")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "El nombre del propietario es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre del propietario no puede superar los 100 caracteres")]
        public string OwnerName { get; set; } = null!;

        [Required(ErrorMessage = "Debe especificar un balance")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Formato de balance inválido. Ej: 0.00")]
        public decimal BalanceAmount { get; set; }

        [Required(ErrorMessage = "Debe especificar el monto de sobregiro")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El sobregiro debe tener formato moneda (0.00)")]
        public decimal OverdraftAmount { get; set; }
    }
}
