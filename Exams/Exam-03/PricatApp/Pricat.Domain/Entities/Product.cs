using Pricat.Domain.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Pricat.Domain.Entities
{
    public partial class Product : EntityBase
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }

    }
}
