﻿using System.ComponentModel.DataAnnotations;

namespace CategoryService.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
