using MoneyBankService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Domain.Entities
{
    public class Account : EntityBase
    {
        [Required(ErrorMessage = "El campo AccountType es requerido.")]

        [RegularExpression("^[AC]$", ErrorMessage = "El campo AccountType solo debe ser 'A' o 'C'.")]

        [StringLength(1, ErrorMessage = "la maxima longitud de AccountType es de un(1) caracter.")]

        public char AccountType { get; set; } = 'A';
        [DataType(DataType.Date, ErrorMessage = "Este campo de creacion debe tener una fecha valida.")]


        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "El campo AccountNumber es requerido.")]

        [MaxLength(10, ErrorMessage = "La maxima longitud de AccountNumber es de 10 caracteres.")]

        [RegularExpression(@"\d{10}", ErrorMessage = "El campo AccountNumber solo acepta numeros.")]


        public string AccountNumber { get; set; } = null!;
        [Required(ErrorMessage = "El campo OwnerName es requerido.")]

        [MaxLength(100, ErrorMessage = "La maxima longitud de OwnerName es de 100 caracteres.")]


        public string OwnerName { get; set; } = null!;
        [Required(ErrorMessage = "El campo BalanceAmount es requerido.")]

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El campo BalanceAmount debe tener un formato de dinero (0.00)")]


        public decimal BalanceAmount { get; set; }
        [Required(ErrorMessage = "El campo OverdraftAmount es requerido.")]

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El campo OverdraftAmount debe tener un formato de dinero (0.00)")]


        public decimal OverdraftAmount { get; set; }
    }
}