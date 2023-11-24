namespace TeamsService.Api.Dtos
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public DateTime Fecha { get; set; }
        public string Categoria { get; set; }
    }
}