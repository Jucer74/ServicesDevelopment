<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Common;
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
=======
﻿using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.Common;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
