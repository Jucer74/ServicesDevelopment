﻿using System.ComponentModel.DataAnnotations;


namespace josoterad.api.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El Email es requerido")]
    [EmailAddress(ErrorMessage = "El valor no es un Email valido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "El nombre  es requerido")]
    [StringLength(50, ErrorMessage = "La longitud máxima del nombre es 50")]
    public string Fullname { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;
}