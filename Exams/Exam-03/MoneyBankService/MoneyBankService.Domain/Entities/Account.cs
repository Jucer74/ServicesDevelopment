using System.ComponentModel.DataAnnotations;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Entities
{
    public class Account : EntityBase
    {
        [Required(ErrorMessage = "El campo Tipo de Cuenta es Requerido.")]
        [RegularExpression("[AC]",ErrorMessage ="El campo Tipo de Cuenta solo permite (A o C)")]
        public char AccountType { get; set; } = 'A';

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El campo Número de Cuenta es Requerido.")]
        [MaxLength(10, ErrorMessage = "El campo Numero de Cuenta tiene una longitud maxima de 10 caractere")]
        [RegularExpression(@"\d{10}", ErrorMessage = "El campo Numero de Cuenta Solo Acepta Numeros ")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "El campo Nombre de Propietario es Requerido.")]
        [MaxLength(100, ErrorMessage = "El campo Nombre de Propietario tiene una longitud maxima de 100 caractere")]
        public string OwnerName { get; set; } = null!;

        [Required(ErrorMessage = "El campo Tipo de Moneda es Requerido.")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El campo Balance debe ser en formato Moneda (0.00)")]
        public decimal BalanceAmount { get; set; } = 0;

        [Required(ErrorMessage = "El campo Sobregiro es Requerido.")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El campo Sobregiro debe ser en formato Moneda (0.00)")]
        public decimal OverdraftAmount { get; set; } = 0;
    }
}