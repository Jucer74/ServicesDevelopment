namespace Arepas.Api.Dtos;

public class CustomerDto
{
    public int Id { get; set; }

    public string UserEmail { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;
}