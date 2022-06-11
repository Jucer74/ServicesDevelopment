using Pricat.Domain.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Pricat.Domain.Entities
{
    public partial class Category: EntityBase
    {
        public Category()
        {
        }

        public string Description { get; set; }

    }
}
