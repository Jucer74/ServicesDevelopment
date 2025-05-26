using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyBankWeb_davhergar.Models
{
    public class AccountDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Cuenta es Requerido")]
        [RegularExpression("[AC]", ErrorMessage = "El campo Tipo de Cuenta solo permite (A o C)")]
        public char AccountType { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El campo Numero de la Cuenta es Requerido")]
        [MaxLength(10, ErrorMessage = "El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo Numero de La Cuenta tiene una longitud minima de 10 caracteres")]
        [RegularExpression(@"\d{10}", ErrorMessage = "El Campo Numero de la Cuenta Solo Acepta Numeros")]
        public string AccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Nombre del Propietario es Requerido")]
        [MaxLength(100, ErrorMessage = "El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres")]
        public string OwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Balance es Requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Balance debe ser mayor a cero")]
        public decimal BalanceAmount { get; set; }

        [Required(ErrorMessage = "El campo Sobregiro es Requerido")]
        public decimal OverdraftAmount { get; set; }
    }
}
