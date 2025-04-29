using System.ComponentModel.DataAnnotations;

namespace Pricat.Domain.Common
{

    public class Entitybase
    {
        [Key] public int Id { get; set; }
    }

}