using System.ComponentModel.DataAnnotations;

namespace TeamsService.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}