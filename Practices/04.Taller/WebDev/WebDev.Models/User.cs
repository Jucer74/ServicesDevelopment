using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDev.Models
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerida")]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Rrequerido")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Username { get; set; }
    }
}
