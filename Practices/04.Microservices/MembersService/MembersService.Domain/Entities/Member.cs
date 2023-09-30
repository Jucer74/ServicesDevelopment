using MembersService.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Domain.Entities
{
    public class Member : EntityBase
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string Position { get; set; } = null!;

        public int TeamId { get; set; }
    }
}
