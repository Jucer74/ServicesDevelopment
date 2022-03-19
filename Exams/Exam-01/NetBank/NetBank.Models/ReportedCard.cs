using System;

namespace NetBank.Models
{
    using System.ComponentModel.DataAnnotations;
    public class ReportedCard
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El IssuingNetwork es obligatorio")]
        public string IssuingNetwork { get; set; }
        [Required(ErrorMessage = "El CreditCardNumber  es obligatorio")]
        public string CreditCardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "El StatusCard es obligatorio")]
        public string StatusCard { get; set; }
        [Required(ErrorMessage = "ReportedDate is required")]
        public DateTime ReportedDate { get; set; }
        [Required(ErrorMessage = "LastUpdatedDate is required")]
        public DateTime LastUpdatedDate { get; set; }



    }
}