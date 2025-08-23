using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Application.Dto
{
    public class TransactionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el número de cuenta")]
        [MaxLength(10, ErrorMessage = "El número de cuenta no puede exceder 10 caracteres")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Solo se permiten 10 dígitos numéricos en el número de cuenta")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "El valor de la transacción es obligatorio")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El valor debe tener el formato correcto de moneda (ej. 0.00)")]
        public decimal ValueAmount { get; set; }
    }
}
