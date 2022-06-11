using Pricat.Domain.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Domain.Entities
{
    public class Categories:Entitybase
    {
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
