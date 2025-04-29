using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Common;

namespace Pricat.Domain.Entities
{
    class Categories : EntityBase
    {
        [Required]
        public string Description { get; set; }


        public ICollection<Products> Productos { get; set; }
    }
}
