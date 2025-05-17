namespace UserManagement.Application.DTOs;

public class CreateUserDto
{
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public required string Password { get; set; }
    public required string UserName { get; set; }
}
