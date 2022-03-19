using System;
using System.ComponentModel.DataAnnotations;

namespace NetBank.Models
{
    public class ReportedCard
    {
        [Key ]
        public int Id { get; set; }

        [Required(ErrorMessage ="campo requerido")]
        public string IssuingNetwork { get; set; }

        [Required(ErrorMessage = "campo requerido")]
        public string CreditCardNumber { get; set;}


        public string FirstName { get; set;  }


        public string LastName { get; set;  }

        [Required(ErrorMessage = "campo requerido")]
        public string StatusCard { get; set; }


        [Required(ErrorMessage = "campo requerido")]
        public DateTime ReportedDate { get; set; }

        [Required(ErrorMessage = "campo requerido")]
        public DateTime LastUpdatedDate { get; set;}
    }

}
