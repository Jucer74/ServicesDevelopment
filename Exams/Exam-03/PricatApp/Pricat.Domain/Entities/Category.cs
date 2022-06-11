using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Common; 

namespace Pricat.Domain.Entities
{
    public class Category : EntityBase
    {

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

    }

}