using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Entities.Base;
namespace Pricat.Domain.Entities
{
    public class Category: EntityBase
    {
        public string? Description { get; set; }
    }
}
