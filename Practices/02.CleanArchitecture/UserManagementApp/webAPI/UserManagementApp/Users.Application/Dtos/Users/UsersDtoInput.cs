using System.ComponentModel.DataAnnotations;

namespace Users.Application.Dtos.Users;

public class UserDtoInput

{
    public int Id { get; set; }
    public string Email { get; set; }

    public string FullName { get; set; }

    public string Password { get; set; }

    public string UserName { get; set; }
}
