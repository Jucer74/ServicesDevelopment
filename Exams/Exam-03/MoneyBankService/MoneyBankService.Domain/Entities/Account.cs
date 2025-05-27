using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Entities
{
    [Table("accounts")]
    public class Account : EntityBase
    {
            [Required(ErrorMessage = "El campo de tipo cuenta es Requerido")]
            [RegularExpression("[AC]")]
            public char AccountType { get; set; } = 'A';

            [DataType(DataType.Date)]
            public DateTime CreationDate { get; set; } = DateTime.Now;

            [Required]
            [MaxLength(10, ErrorMessage = "El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")]
            [RegularExpression(@"\d{10}", ErrorMessage = "El Campo Numero de la Cuenta Solo Acepta Numeros")]
            public string AccountNumber { get; set; } = null!;

            [Required(ErrorMessage = "El campo Nombre del Propietario es Requerido")]
            [MaxLength(100, ErrorMessage = "El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres\"")]
            public string OwnerName { get; set; } = null!;

            [Required(ErrorMessage = "El campo Balance es Requerido")]
            [RegularExpression(@"^\d+\.?\d{0,2}$", ErrorMessage = "El campo Balance debe ser en formato Moneda (0.00)")]
            [Column(TypeName = "decimal(18,2)")]
            public decimal BalanceAmount { get; set; }

            [RegularExpression(@"^\d+\.?\d{0,2}$", ErrorMessage = "El campo Balance debe ser en formato Moneda (0.00)")]
            [Column(TypeName = "decimal(18,2)")]
            public decimal OverdraftAmount { get; set; }
    }
}