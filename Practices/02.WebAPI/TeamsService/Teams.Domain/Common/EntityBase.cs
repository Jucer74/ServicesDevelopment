﻿using System.ComponentModel.DataAnnotations;

namespace Teams.Domain.Common;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}