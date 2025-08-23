using System.ComponentModel.DataAnnotations;

namespace MoneyBankService02.Application.Dto;

public class TransactionDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Debe ingresar el número de cuenta")]
    [MaxLength(10, ErrorMessage = "Máximo 10 caracteres")]
    [RegularExpression(@"\d{10}", ErrorMessage = "Debe contener exactamente 10 dígitos")]
    public string AccountNumber { get; set; } = null!;

    [Required(ErrorMessage = "El valor de la transacción es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "Debe ser un monto válido")]
    public decimal ValueAmount { get; set; }
}

