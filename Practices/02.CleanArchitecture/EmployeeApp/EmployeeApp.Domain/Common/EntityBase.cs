﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}