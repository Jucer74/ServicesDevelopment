using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Entities.Base;

namespace Pricat.Domain.Entities
{
    public class Product: EntityBase
    {
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? EanCode { get; set; }
        public string? Description { get; set; }
        public string? Unit { get; set; }
        public double Price { get; set; }

    }
}
