using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Common;

namespace Pricat.Domain.Entities
{
    class Products : EntityBase
    {
        [Required]
        public string EanCode { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public Categories Categoria { get; set; }
    }
}
