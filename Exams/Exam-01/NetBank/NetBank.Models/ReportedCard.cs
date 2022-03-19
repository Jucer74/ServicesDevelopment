using System;
using System.ComponentModel.DataAnnotations;

namespace NetBank.Models
{    public class ReportedCard
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "is required")]
        public string IssuingNetwork { get; set; }

        [Required(ErrorMessage = "is required")]
        public string CreditCardNumber { get; set; }

        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        [Required(ErrorMessage = "is required")]
        public string StatusCard { get; set; }

        [Required(ErrorMessage = "is required")]
        public System.DateTime ReportedDate { get; set; }

        [Required(ErrorMessage = "is required")]
        public System.DateTime LastUpdatedDate { get; set; }
    }
}
