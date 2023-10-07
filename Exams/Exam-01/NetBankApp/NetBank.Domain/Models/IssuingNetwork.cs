<<<<<<< HEAD
using NetBank.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetBank.Domain.Dto;

namespace NetBank.Domain.Models;

[Table("issuingnetworks")]
public class IssuingNetwork:EntityBase
{   
=======
﻿using NetBank.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace NetBank.Domain.Models;

public class IssuingNetwork:EntityBase
{
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
    [Required]
    public string Name { get; set; } = null!;

    public string? StartsWithNumbers { get; set; } = null!;

    public string? InRange { get; set; } = null!;

    [Required]
    public string AllowedLengths { get; set; } = null!;
}