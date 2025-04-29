using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace UserManagement.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    
    }
}
