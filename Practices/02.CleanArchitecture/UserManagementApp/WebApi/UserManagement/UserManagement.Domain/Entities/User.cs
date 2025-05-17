<<<<<<< HEAD
﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
=======
﻿using System.ComponentModel.DataAnnotations;
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities;

public class User : EntityBase
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

<<<<<<< HEAD
    [Required ]
    public string FullName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string UserName { get; set; }
=======
    [Required]
    public string Fullname { get; set; }

    [Required]
        public string Password { get; set; }

    [Required]
    public string Username { get; set; }
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
}