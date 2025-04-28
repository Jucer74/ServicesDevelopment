using System.ComponentModel.DataAnnotations;

namespace TeamsApi.Dominio.Common
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
