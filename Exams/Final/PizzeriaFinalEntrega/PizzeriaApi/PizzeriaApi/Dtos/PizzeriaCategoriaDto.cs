namespace PizzeriaApi.Dtos;

public class PizzeriaCategoriaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Tamaño { get; set; } = null!;
    public string Precio { get; set; } = null!;
    public int PizzasId { get; set; }

}
