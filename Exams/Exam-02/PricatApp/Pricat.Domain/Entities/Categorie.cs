using PricatApp.Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace PricatApp.Domain.Entities
{
    public class Categorie : EntityBase
    {
        [Required]
        public string? Description { get; set; }
    }
}