using System.ComponentModel.DataAnnotations;
using TodoSystem.Enums;

namespace TodoSystem.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public StatusEnum Status { get; set; }
    }
}
