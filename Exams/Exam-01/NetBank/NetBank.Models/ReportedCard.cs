namespace NetBank.Models
{
    using System.ComponentModel.DataAnnotations;
    public class ReportedCard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string IssuingNetwork { get; set; }

        [Required(ErrorMessage = "Required")]
        public string CreditCardNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string StatusCard { get; set; }

        [Required(ErrorMessage = "Required")]
        public System.DateTime ReportedDate { get; set; }

        [Required(ErrorMessage = "Required")]
        public System.DateTime LastUpdatedDate { get; set; }
    }
}
