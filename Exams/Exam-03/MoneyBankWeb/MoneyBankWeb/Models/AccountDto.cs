using System.ComponentModel.DataAnnotations;

namespace MoneyBankWeb_josoterad.Models
{
    public class AccountDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe especificar el tipo de cuenta")]
        [RegularExpression("[AC]", ErrorMessage = "Solo se permite A o C como tipo de cuenta")]
        public char AccountType { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Se requiere el número de cuenta")]
        [MaxLength(10, ErrorMessage = "Máximo 10 caracteres para el número de cuenta")]
        [RegularExpression(@"\d{10}", ErrorMessage = "El número de cuenta debe contener exactamente 10 dígitos")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "El nombre del propietario es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre del propietario no puede superar los 100 caracteres")]
        public string OwnerName { get; set; } = null!;

        [Required(ErrorMessage = "Debe especificar un balance")]
        [Range(0, double.MaxValue, ErrorMessage = "El balance debe ser mayor o igual a 0")]
        public decimal BalanceAmount { get; set; }

        [Required(ErrorMessage = "Debe especificar el monto de sobregiro")]
        [Range(0, double.MaxValue, ErrorMessage = "El sobregiro debe ser mayor o igual a 0")]
        public decimal OverdraftAmount { get; set; }
    }
}
