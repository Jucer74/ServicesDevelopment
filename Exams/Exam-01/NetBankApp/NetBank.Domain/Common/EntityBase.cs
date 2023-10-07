<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;

namespace NetBank.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
=======
﻿using System.ComponentModel.DataAnnotations;

namespace NetBank.Domain.Common
{
   public abstract class EntityBase
   {
      [Key]
      [Required]
      public int Id { get; set; }
   }
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
}