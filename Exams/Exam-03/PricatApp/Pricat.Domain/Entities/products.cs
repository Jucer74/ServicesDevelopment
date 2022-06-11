using Pricat.Domain.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Domain.Entities
{
    public class Products:Entitybase
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(13)]
        public string EanCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; }
        [Required]
        [MaxLength(13)]
        [MinLength(2)]
        public Decimal Price { get; set; }
    }
}
