using System.ComponentModel.DataAnnotations;

namespace MoneyBankAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Numero de la Cuenta es Requerido")]
        [MaxLength(10, ErrorMessage = "El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")]
        [RegularExpression(@"\d{10}", ErrorMessage = "El Campo Numero de la Cuenta Solo Acepta Numeros")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "El campo Valor es Requerido")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El campo Valor debe ser en formato Moneda (0.00)")]
        public decimal ValueAmount { get; set; }
    }
}
