using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Pricat.Domain.common
{
    public abstract class Entitybase
    {
        [Key]
        public int Id { get; set; }
    }
}
