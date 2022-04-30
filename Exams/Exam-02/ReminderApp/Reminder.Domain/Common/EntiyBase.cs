using System.ComponentModel.DataAnnotations;

namespace ReminderAPP.Domain.Common
{
    public abstract class EntiyBase
    {
        [Key]
        public int Id { get; set; }
    }
}
